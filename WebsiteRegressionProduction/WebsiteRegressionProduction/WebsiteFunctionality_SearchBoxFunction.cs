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
    class WebsiteFunctionality_SearchBoxFunction : Test
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
            catch (Exception) { } //ignore exceptions when trying to close the browser

            TearDownTestGeneric();
        }

        [Test]
        public void TheWebsiteFunctionality_SearchBoxFunctionTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            batch = driver.CaptureBatchNumberExternallyClaims();
            Helper.Process5010Claims(batch);
            driver.Navigate().Refresh();
            int timeout = 0;
            bool isFound = false;
            bool isDuplicate = false;
            isFailedTest = false;

            while (timeout < 60 && !isFound && !isDuplicate && !isFailedTest)
            {
                Thread.Sleep(1000);
                driver.Navigate().Refresh();
                isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
                isDuplicate = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
            }

            if (isDuplicate)
                Helper.UpdateDuplicateToFailed(package, batch);
            driver.Navigate().Refresh();

            if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton")))
            {
                Helper.UpdateBatchToActive(batch);
                driver.Navigate().Refresh();
            }
            try
            {
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchBoxTB"), 10).Clear();
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchBoxTB")).SendKeys("Woodmanseezz");
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchButn"), 10).Click();
            }
            catch (Exception)
            {
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchBoxTB"), 10).Clear();
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchBoxTB")).SendKeys("Woodmanseezz");
                driver.FindElement(By.Id("ctl00_LeftContent_TrackSearchMenu_SearchBoxCtrl_SearchButn"), 10).Click();
            }

            bool patientFound = false;
            for (int i = 3; i < 17; i++)
            {
                string batchElement = "ctl00_MainContent_ctl00_TrackClaims_ctl0" + i + "_BatchNumber";
                try
                {
                    string currentBatch = driver.FindElement(By.Id(batchElement), 10).Text;
                    if (currentBatch.Equals(batch))
                    {
                        patientFound = true;
                        driver.FindElement(By.Id(batchElement), 10).Click();
                        break;
                    }
                }
                catch (NoSuchElementException)
                {
                    break;
                }
            }
            if (!patientFound)
            {
                verificationErrors.Append("Unable to find batch + Patient combination");
                driver.FindElement(By.Id("track_link")).Click();
            }
            else
            {
                driver.FindElement(By.Id("patientCtrl_tbLastName")).Clear();
                driver.FindElement(By.Id("patientCtrl_tbLastName")).SendKeys("NewLastNameForTest");
                driver.FindElement(By.Id("subscriberCtrl_tbLastName")).Clear();
                driver.FindElement(By.Id("subscriberCtrl_tbLastName")).SendKeys("NewLastNameForTest");
                driver.FindElement(By.Id("btnSaveTop")).Click();
            }
            endOfTest();
        }
    }
}
