using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OneTouchUploadProduction;
using TestLibrary;

namespace WebsiteRegressionProduction
{
    [TestFixture]
    public class WebsiteFunctionalityPartII : Test
    {
        private const string FILEUNDERTEST = @"";   //NO files under test; Part II is tests that do not require any uploading
        private const DocumentType type = DocumentType.MedicalClaim;
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.StatementClient;
            driver = new FirefoxDriver();
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);

            SetupTestGeneric();

            if (!driver.Login(client))
            {
                verificationErrors.Append(
                    "Unable to login for test WebsiteFunctionalityPartII");
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
            catch (Exception){} //ignore exceptions when trying to close the browser

            TearDownTestGeneric(); 
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Insurance_Response_Tab_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.CssSelector("li.claim-level > a > span.claim-tab-label.red")).Click();
            driver.FindElement(By.Id("ctl00_LeftContent_NewReportsCol1_ctl02_NameItem"), 10).Click();

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Electronic_EOBs_Tab_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.CssSelector("a.last > span.claim-tab-descrip"), 10).Click();
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_MainContent_DownloadBtn"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_MainContent_DeleteBtn")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("", driver.FindElement(By.XPath("//input[@value='Print']")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Apex_Inbox_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("ctl00_NavigationBar_A2")).Click();
            driver.FindElement(By.Id("ctl00_LeftContent_NewReportsCol1_ctl02_NameItem"),10).Click();
            driver.FindElement(By.Id("ctl00_PageNavigation_InvoicesTabBtn"), 10).Click();
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_LeftContent_btnSelectAll"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_LeftContent_btnUnSelectAll")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_LeftContent_MultiDeleteBtn")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_PageNavigation_ApexTabBtn")).Click();
            driver.FindElement(By.Id("ctl00_LeftContent_btnSelectAll"), 10).Click();
            try
            {
                Assert.AreEqual("on",
                    driver.FindElement(By.Id("ctl00_LeftContent_NewReportsCol1_ctl02_DeleteCheckBox"), 10)
                        .GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("on",
                    driver.FindElement(By.Id("ctl00_LeftContent_NewReportsCol1_ctl03_DeleteCheckBox"))
                        .GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_LeftContent_btnUnSelectAll")).Click();

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Upload_Page_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("upload_link"), 10).Click();
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_LeftContent_ClaimManualFileUpload"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("", driver.FindElement(By.Id("ctl00_LeftContent_StatementManualFileUpload")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Help_Page_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.LinkText("Help")).Click();
            
            /* TODO Test needs to be updated since the new Help Center was Released To Production  -DTE */
            //try
            //{
            //    Assert.AreEqual("Remote Support",
            //        driver.FindElement(By.Id("ctl00_MainContent_Section3Control0_SectionTitle2"), 10).Text);
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
            //try
            //{
            //    Assert.AreEqual("Frequently Asked Questions (FAQ)", driver.FindElement(By.Id("H1")).Text);
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Manage_Account_Page_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("ctl00_NavigationBar_ClientIDLabel")).Click();
            driver.FindElement(By.Id("manage_link"), 5).Click();
            try
            {
                Assert.AreEqual("Select the claim form section(s) you want to show up on the 5010 claim form:",
                    driver.FindElement(By.CssSelector("#ctl00_MainContent_tcMainContainer_tabPanelClaim > div > div"),
                        10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelClaim_rptHeader_ctl09_cb")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelClaim_btnSaveHeader"), 10).Click();
            try
            {
                Assert.AreEqual("on",
                    driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelClaim_rptHeader_ctl09_cb"), 10)
                        .GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelClaim_rptHeader_ctl09_cb")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelClaim_btnSaveHeader"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelLines_rptLineItems_ctl14_cb"), 10)
                .Click();
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelLines_btnSaveLines"), 10).Click();
            try
            {
                Assert.AreEqual("on",
                    driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelLines_rptLineItems_ctl14_cb"),
                        10).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelLines_rptLineItems_ctl14_cb")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelLines_btnSaveLines"), 10).Click();
            try
            {
                Assert.AreEqual("1/16/2013 7:57:50 AM",
                    driver.FindElement(By.Id("ctl00_MainContent_tcMainContainer_tabPanelTandC_TCAgreedDate"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            endOfTest();
        }

        [Test]
        public void TheWebsiteFunctionalityPartII_Client_Logoff_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("ctl00_NavigationBar_ClientIDLabel")).Click();
            driver.FindElement(By.Id("logout_link"),10).Click();
            try
            {
                Assert.AreEqual("Login", driver.FindElement(By.CssSelector("p.text18.btmar15"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

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
