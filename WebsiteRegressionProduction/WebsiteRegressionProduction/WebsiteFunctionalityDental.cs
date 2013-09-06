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
    class WebsiteFunctionalityDental : Test
    {
        private const string FILEUNDERTEST = @"Files\Dental5010.BNL";
        private const DocumentType type = DocumentType.DentalClaim;
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.DentalClient5010;
            driver = new FirefoxDriver();
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);

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
        public void TheCreateNewClaimOnlineTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            batch = driver.CaptureBatchNumberExternallyClaims();
            bool isFound = Helper.Process5010Claims(batch);
            driver.Navigate().Refresh();
            isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
            int timeout = 0;
            while (!isFound && !isFailed && timeout < 5)
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
                timeout++;
            }
            if (isFailed)
            {
                verificationErrors.Append("Claim(s) in an unexpected Fail state");
                Assert.Fail("Dental5010 batch uploaded with an unexpected status of FAIL");
            }


            for (int second = 0; ; second++)
            {
                if (second >= 90) Assert.Fail("timeout");
                try
                {
                    isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
                    if (isFailed)
                    {
                        verificationErrors.Append("Claim(s) in an unexpected Fail state");
                    }
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


            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ReadyLinkButton")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddClaimStatementButton"), 10).Click();

            driver.FindElement(By.Id("payerCtrl_tbName")).Clear();
            driver.FindElement(By.Id("payerCtrl_tbName")).SendKeys("BLUE CROSS BLUE SHIELD FEDERAL");
            driver.FindElement(By.Id("payerCtrl_tbAddress1")).Clear();
            driver.FindElement(By.Id("payerCtrl_tbAddress1")).SendKeys("P.O. BOX 30270");
            driver.FindElement(By.Id("payerCtrl_tbCity")).Clear();
            driver.FindElement(By.Id("payerCtrl_tbCity")).SendKeys("SALT LAKE CITY");
            new SelectElement(driver.FindElement(By.Id("payerCtrl_ddlState"))).SelectByText("UT");
            driver.FindElement(By.Id("payerCtrl_tbZip")).Clear();
            driver.FindElement(By.Id("payerCtrl_tbZip")).SendKeys("84130");
            new SelectElement(driver.FindElement(By.Id("electronicPayerCtrl_ddlOutputSubs"))).SelectByText("BCBS UT (Utah)");
            driver.FindElement(By.Id("subscriberCtrl_tbFirstName")).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbFirstName")).SendKeys("BUSTER");
            driver.FindElement(By.Id("subscriberCtrl_tbMiddleName")).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbMiddleName")).SendKeys("B");
            driver.FindElement(By.Id("subscriberCtrl_tbLastName")).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbLastName")).SendKeys("BLUTHE");
            driver.FindElement(By.Id("subscriberCtrl_tbSubscriberID")).Clear();
            driver.FindElement(By.Id("subscriberCtrl_tbSubscriberID")).SendKeys("R50311229");
            driver.FindElement(By.Id("patientCtrl_cbRelationshipDependent")).Click();
            driver.FindElement(By.Id("patientCtrl_tbFirstName")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbFirstName")).SendKeys("MOTHER");
            driver.FindElement(By.Id("patientCtrl_tbMiddleName")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbMiddleName")).SendKeys("J");
            driver.FindElement(By.Id("patientCtrl_tbLastName")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbLastName")).SendKeys("SNAPPYLEATHERFACE");
            driver.FindElement(By.Id("patientCtrl_tbAddress1")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbAddress1")).SendKeys("1 CHEAP MINI MANSION DRIVE");
            driver.FindElement(By.Id("patientCtrl_tbCity")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbCity")).SendKeys("SUDDEN VALLEY");
            new SelectElement(driver.FindElement(By.Id("patientCtrl_ddlState"))).SelectByText("UT");
            driver.FindElement(By.Id("patientCtrl_tbZip")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbZip")).SendKeys("84699");
            driver.FindElement(By.Id("patientCtrl_tbDateOfBirth")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbDateOfBirth")).SendKeys("01/02/2002");
            
            driver.FindElement(By.Id("patientCtrl_rblSex_1")).Click();
            driver.FindElement(By.Id("patientCtrl_tbPatientAccountNumber")).Clear();
            driver.FindElement(By.Id("patientCtrl_tbPatientAccountNumber")).SendKeys("987654321");
            driver.FindElement(By.Id("lineCtrl1_tbDateFrom")).Clear();
            driver.FindElement(By.Id("lineCtrl1_tbDateFrom")).SendKeys("08/08/2013");
            
            driver.FindElement(By.Id("lineCtrl1_toothControl1_tbToothNumber")).Clear();
            driver.FindElement(By.Id("lineCtrl1_toothControl1_tbToothNumber")).SendKeys("12");
            driver.FindElement(By.Id("lineCtrl1_surfaceControl1_tbSurface1")).Clear();
            driver.FindElement(By.Id("lineCtrl1_surfaceControl1_tbSurface1")).SendKeys("O");
            driver.FindElement(By.Id("lineCtrl1_tbProcedureCode")).Clear();
            driver.FindElement(By.Id("lineCtrl1_tbProcedureCode")).SendKeys("D2391");
            driver.FindElement(By.Id("lineCtrl1_tbDescription")).Clear();
            driver.FindElement(By.Id("lineCtrl1_tbDescription")).SendKeys("FIRST TOOTH DOWN");
            driver.FindElement(By.Id("lineCtrl1_tbFee")).Clear();
            driver.FindElement(By.Id("lineCtrl1_tbFee")).SendKeys("100.00");
            driver.FindElement(By.Id("lbAddNewLine")).Click();
            driver.FindElement(By.Id("lineCtrl2_toothControl1_tbToothNumber"),10).Clear();
            driver.FindElement(By.Id("lineCtrl2_toothControl1_tbToothNumber")).SendKeys("21");
            driver.FindElement(By.Id("lineCtrl2_tbDateFrom")).Clear();
            driver.FindElement(By.Id("lineCtrl2_tbDateFrom")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("lineCtrl2_surfaceControl1_tbSurface1")).Clear();
            driver.FindElement(By.Id("lineCtrl2_surfaceControl1_tbSurface1")).SendKeys("D");
            driver.FindElement(By.Id("lineCtrl2_surfaceControl1_tbSurface2")).Clear();
            driver.FindElement(By.Id("lineCtrl2_surfaceControl1_tbSurface2")).SendKeys("O");
            driver.FindElement(By.Id("lineCtrl2_tbProcedureCode")).Clear();
            driver.FindElement(By.Id("lineCtrl2_tbProcedureCode")).SendKeys("D2392");
            driver.FindElement(By.Id("lineCtrl2_tbDescription")).Clear();
            driver.FindElement(By.Id("lineCtrl2_tbDescription")).SendKeys("MULTIPLE TOOTH SURFACE MULTIPLE NEW LINES ADDED AUTOMATICALLY");
            driver.FindElement(By.Id("lineCtrl2_tbFee")).Clear();
            driver.FindElement(By.Id("lineCtrl2_tbFee")).SendKeys("900.00");
            driver.FindElement(By.Id("missingTeethCtrl_tbTotalFee")).Clear();
            driver.FindElement(By.Id("missingTeethCtrl_tbTotalFee")).SendKeys("1000.00");
            new SelectElement(driver.FindElement(By.Id("ancillaryCtrl_ddlPlaceOfServiceCode"))).SelectByText("Office");
            driver.FindElement(By.Id("authorizationsCtrl_cbPatSigOnFile")).Click();
            driver.FindElement(By.Id("authorizationsCtrl_cbSubSigOnFile")).Click();
            driver.FindElement(By.Id("treatingDentistCtrl_cbProviderSignature")).Click();
            driver.FindElement(By.Id("btnSubmit")).Click();

            try
            {
                Assert.AreEqual("This claim has passed all validations and is ready for processing.", driver.FindElement(By.CssSelector("center > div"), 10).Text);
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
