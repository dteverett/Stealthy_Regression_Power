using System;
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


namespace WebsiteRegressionProduction
{
    class Medical5010 : Test
    {
        private const string FILEUNDERTEST = @"Files\5010Medical.HLD";
        private const DocumentType type = DocumentType.MedicalClaim;
        private IWebDriver driver;
        
        [SetUp]
        public void SetupTest()
        {
            client = Credentials.MedicalClient5010;
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);
            bool isUploaded = false;
            driver = new FirefoxDriver();
            SetupTestGeneric();

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
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void UploadAndProcessMedical5010Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            batch = driver.CaptureBatchNumberExternallyClaims();
            bool isFound = Helper.Process5010Claims(batch);
            driver.Navigate().Refresh();
            isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
            int timeout = 0;
            while(!isFound && !isFailed && timeout < 5)
            {
                isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
                if (!isFound)
                {
                    isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
                    if (!isFound)
                    {
                        isFound = Helper.Process5010Claims(batch);
                        driver.Navigate().Refresh();
                    }
                }
                isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
                timeout++;
            }
            if (isFailed)
            {
                verificationErrors.Append("Claim(s) in unexepected failed status");  //Actually should be failing until Provider_T record is updated for ZZZ to include HLD values
            }

            for (int second = 0; ; second++)
            {
                if (second >= 90) Assert.Fail("timeout");
                try
                {
                    isDuplicate = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
                    if (!isDuplicate)
                    {
                        if (
                            driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton")))
                        {
                            Helper.UpdateProcessedToReady(package, batch);
                            break;    
                        }
                    }
                    else
                    {
                        Helper.UpdateDuplicates(batch, package);
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
                driver.Navigate().Refresh();
            }
            driver.Navigate().Refresh();
            
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyImage"), 5).Click();

            string batchCheck = "Batch " + batch;
            try
            {
                Assert.AreEqual(batchCheck, driver.FindElement(By.Id("ctl00_BreadCrumbContent_TrackBreadCrumb_BatchNumLabel"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl04_PatientName")).Click();

            try
            {
                Assert.AreEqual("This claim has passed all validations and is ready for processing.", driver.FindElement(By.CssSelector("center > div"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("patientCtrl_tbFirstName")).Clear();
            driver.FindElement(By.Id("btnSaveTop")).Click();
            try
            {
                Assert.AreEqual("Patient First Name is missing.", driver.FindElement(By.CssSelector("#blFailedValidations > li"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("patientCtrl_tbFirstName")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbFirstName")).SendKeys("David");
            driver.FindElement(By.Id("btnSaveTop")).Click();
            driver.FindElement(By.Id("taxIDCtrl_tbTotalCharge"), 10).Clear();
            driver.FindElement(By.Id("taxIDCtrl_tbTotalCharge")).SendKeys("445.45");
            driver.FindElement(By.Id("btnSaveTop")).Click();
            driver.FindElement(By.Id("lineCtrl1_tbCharges"), 10).Clear();
            driver.FindElement(By.Id("lineCtrl1_tbCharges")).SendKeys("999.00");
            driver.FindElement(By.Id("btnSaveTop")).Click();
            try
            {
                Assert.AreEqual("$999.00", driver.FindElement(By.Id("taxIDCtrl_tbTotalCharge"), 10).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("$999.00", driver.FindElement(By.Id("taxIDCtrl_tbBalanceDue"), 5).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("prevNextClaimCtrl_hlNext")).Click();
            try
            {
                Assert.AreEqual("LUNDBERG", driver.FindElement(By.Id("patientCtrl_tbLastName"), 10).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("prevNextClaimCtrl_hlBatch")).Click();
            try
            {
                Assert.AreEqual("Woodmansee, William D", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl13_PatientName"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }
    }
}
