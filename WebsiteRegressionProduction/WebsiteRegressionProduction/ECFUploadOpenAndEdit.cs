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
    /// <summary>
    /// This class contains all tests for the ECF claims processing service.  This class tests the OneTouch website, as well as the importing and claims
    /// services.  These services and pieces are not meant to be tested here comprehensively; only the ECF process is tested comprehensively
    /// This class extends the Superclass Test and uses the Selenium Webdriver
    /// </summary>
    [TestFixture]
    public class ECFUploadOpenAndEdit : Test
    {
        private const string FILEUNDERTEST = @"Files\3000000001.MZF";
        private const DocumentType type = DocumentType.DentalClaim;
        private IWebDriver driver;

        /// <summary>
        /// Basic Setup with client configured to ZZD, who is configured in the ECF service as an ECF client.  If tests start failing, verify
        /// that client ZZD is still an ECF client by checking the client configuration document located at: \\apexservices1\apexservices\importing\templates\ClientConfiguration.xml
        /// </summary>
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

        /// <summary>
        /// TearDown closes the Webdriver and calls superclass method TearDownTestGeneric
        /// </summary>
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
        /// Uploads an ECF batch of 2 claims that should have no errors.  Once the batch has uploaded and been processed by the services, the test
        /// navigates to the claims on OneTouch and makes several edits to the form and then saves to ensure full functionality of our website to 
        /// ECF clients
        /// </summary>
        [Test]
        public void TheECFUploadOpenAndEditTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            bool isFound = false;
            int timeout = 0;
            isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
            while (!isFound && !isFailed && timeout < 50)
            {
                if (!isFound)
                {
                    isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
                    if (!isFound)
                        isFound =
                            driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
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
