﻿using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OneTouchUploadProduction;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TestLibrary;


namespace WebsiteRegressionProduction_InternetExplorer
{
    [TestFixture]
    public class ECFUploadOpenAndEdit : Test
    {
        private const string FILEUNDERTEST = @"Files\3000000001.MZF";
        private const DocumentType type = DocumentType.DentalClaim;
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.DentalClientECF;
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);
            driver = new FirefoxDriver();
            SetupTestGeneric();

            bool isUploaded = false;
            while (!isUploaded)
            {
                isUploaded = Helper.UploadBatch(package);
            }
            if (!driver.Login(client))
            {
                //TODO: Add logging or console
            }

        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
                Helper.DeleteBatch(batch, package);
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            TearDownTestGeneric(); 
        }

        [Test]
        public void TheECFUploadOpenAndEditTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            bool isFound = false;
            int timeout = 0;
            isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
            while (!isFound && !isFailed && timeout < 50)
            {
                isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
                if (!isFound)
                {
                    isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
                }
                timeout++;
                driver.Navigate().Refresh();
            }

            if (isFailed)
            {
                Assert.Fail("ECF Batch uploaded with an Unexpected Status of Failed");
            }

            driver.Navigate().Refresh();

            if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyImage")))
            {
                bool isDuplicate = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateImage"), 5);
                if (isDuplicate)
                {
                    batch = driver.CaptureBatchNumberExternallyClaims();
                    Helper.UpdateDuplicates(batch, package);
                }
                driver.Navigate().Refresh();

                if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyLinkButton"), 5))
                {
                    verificationErrors.Append("Error 01");
                }
            }

            try
            {
                Assert.AreEqual("2", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyImage"), 5).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyLinkButton"), 5).Click();
            try
            {
                Assert.AreEqual("Dover, Madison M", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"), 5).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"), 5).Click();

            // ******************* Capture batch Number Here ****************** //
            batch = driver.CaptureBatchNumberClaims();

            driver.FindElement(By.Id("subscriberCtrl_tbFirstName"), 5).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbFirstName")).SendKeys("BIANNIAZ");
            driver.FindElement(By.Id("btnSaveTop")).Click();


            try
            {
                Assert.AreEqual("Insured/Subscriber First Name must match Patient First Name if relationship is self.", driver.FindElement(By.CssSelector("#blFailedValidations > li"), 5).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("patientCtrl_tbFirstName"), 5).Clear();
            driver.FindElement(By.Id("patientCtrl_tbFirstName"), 5).SendKeys("BIANNIAZ");
            driver.FindElement(By.Id("subscriberCtrl_tbSubscriberID")).Clear();
            driver.FindElement(By.Id("btnSaveTop"), 5).Click();
            try
            {
                Assert.AreEqual("Insured/Subscriber ID is missing.", driver.FindElement(By.CssSelector("#blFailedValidations > li"), 5).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("subscriberCtrl_tbSubscriberID"), 5).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbSubscriberID")).SendKeys("04578533321");
            driver.FindElement(By.Id("btnSaveTop")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if ("This claim has passed all validations and is ready for processing." == driver.FindElement(By.CssSelector("center > div"), 5).Text) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("prevNextClaimCtrl_hlNext"), 5).Click();
            try
            {
                Assert.AreEqual("PAMULA", driver.FindElement(By.Id("subscriberCtrl_tbFirstName"), 5).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("prevNextClaimCtrl_hlBatch"), 5).Click();
            driver.FindElement(By.Id("ctl00_BreadCrumbContent_TrackBreadCrumb_RootButton"), 5).Click();

            endOfTest();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }

}
