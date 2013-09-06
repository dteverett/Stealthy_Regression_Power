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
    [TestFixture]
    public class FailedByApex : Test
    {
        private const string FILEUNDERTEST = @"Files\Medical5010Fail.HLD";
        private const DocumentType type = DocumentType.MedicalClaim;
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.MedicalClient5010;
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);
            driver = new FirefoxDriver();
            bool isUploaded = false;

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

        [Test]
        public void TheFailedByApexTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            Helper.EliminateTestAccountFailedClaims();
            batch = driver.CaptureBatchNumberExternallyClaims();
            Helper.Process5010Claims(batch);
            driver.Navigate().Refresh();
            int timeout = 0;
            bool isFound = false;
            bool isDuplicate = false;
            isFailedTest =
                    driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
            if (!isFailedTest)
                isFailedTest =
                    driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyLinkButton"));
            while (timeout < 60 && !isFound && !isDuplicate && !isFailedTest)
            {
                Thread.Sleep(1000);
                driver.Navigate().Refresh();
                isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
                isDuplicate = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
                isFailedTest = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
                if (!isFailedTest)
                    isFailedTest = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyLinkButton"));
            }
            if (isFailedTest)
            {
                Assert.Fail("Batch Uploaded with Unexpected Status.  Expected \"Failed\" but was \"Processed\" or \"Ready\"");
            }

            if(isDuplicate)
                Helper.UpdateDuplicateToFailed(package, batch);
            driver.Navigate().Refresh();

            if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton")))
            {
                Helper.UpdateBatchToActive(batch);
                driver.Navigate().Refresh();
            }

            
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchNumberItem"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl13_PatientName"), 10).Click();

            driver.FindElement(By.Id("patientCtrl_tbLastName"),10).Clear();
            driver.FindElement(By.Id("patientCtrl_tbLastName")).SendKeys("NewLastNameForTest");
            driver.FindElement(By.Id("subscriberCtrl_tbLastName")).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbLastName")).SendKeys("NewLastNameForTest");
            driver.FindElement(By.Id("btnSaveTop")).Click();
            try
            {
                Assert.AreEqual("Insured/Subscriber Address is missing or invalid.", driver.FindElement(By.CssSelector("#blFailedValidations > li"), 15).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("The Subscriber must contain a valid address under Subscriber or Patient when relationship is self.", driver.FindElement(By.XPath("//ul[@id='blFailedValidations']/li[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Patient Address is missing or invalid.", driver.FindElement(By.XPath("//ul[@id='blFailedValidations']/li[3]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("span.claim-tab-label.red")).Click();
            try
            {
                Assert.AreEqual("(Not Found)", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"),5).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim"),10).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim"),10).Click();
            try
            {
                Assert.AreEqual("Lundberg, O", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SelectAllClaimsButton")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_DeleteSelectedClaimsButton"),10).Click();
            try
            {
                Assert.AreEqual("No transactions found matching...", driver.FindElement(By.CssSelector("#ctl00_MainContent_ctl00_TrackClaims > tbody > tr > td"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("track_link")).Click();

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
