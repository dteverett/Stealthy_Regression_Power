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
    /// <summary>
    /// This class of tests checks the functionality of the website.  These tests are grouped as such because no file upload is required
    /// This class extends the Test parent class
    /// </summary>
    [TestFixture]
    public class WebsiteFunctionalityPartII : Test
    {
        private const string FILEUNDERTEST = @"";   //NO files under test; Part II is tests that do not require any uploading
        private const DocumentType type = DocumentType.MedicalClaim;
        private IWebDriver driver;

        /// <summary>
        /// Initializes the client used to logon to OneTouch and the webdriver, and then calls SetupTestGeneric() from the Test parent class
        /// Logs into the OneTouch website
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            client = Credentials.StatementClient;
            driver = new FirefoxDriver();
            //package = packageFactory.createPackage(client, FILEUNDERTEST, type);

            SetupTestGeneric();

            if (!driver.Login(client))
            {
                verificationErrors.Append(
                    "Unable to login for test WebsiteFunctionalityPartII");
            }
        }

        /// <summary>
        /// Shuts down the WebDriver and then calls TearDownTestGeneric() from the Test parent class
        /// </summary>
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception){} //ignore exceptions when trying to close the browser

            TearDownTestGeneric(); 
        }

        /// <summary>
        /// This test navigates to the Insurance Response tab and verifies that the page is displaying as expected
        /// If this test is failing, verify the existance of at least one Report in this view -DTE 9/16/13
        /// </summary>
        [Test]
        public void TheWebsiteFunctionalityPartII_Insurance_Response_Tab_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.CssSelector("li.claim-level > a > span.claim-tab-label.red")).Click();
            driver.FindElement(By.Id("ctl00_LeftContent_NewReportsCol1_ctl02_NameItem"), 10).Click();

            endOfTest();
        }

        /// <summary>
        /// This test navigates to the Electronic EOBs tab and verifies that the page resolves as expected by looking for the 
        /// following elements: Download button, Delete button, Print button
        /// </summary>
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

        /// <summary>
        /// This test navigates to the Apex Inbox page on the OneTouch website and verifies expected elements are seen on the page.
        /// This test also tests the functionality of the select/unselect buttons. 
        /// If this test has broken, verify that at least two documents are in the Apex Inbox -DTE 9/16/13
        /// </summary>
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

        /// <summary>
        /// This test navigates to the UPLOAD page on the OneTouch website and verifies elements on the page are visible as expected
        /// Currently this method tests no functionality
        /// </summary>
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

        /// <summary>
        /// This requires update since the deployment of the Apex Help Center
        /// -DTE 9/16/13
        /// </summary>
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

        /// <summary>
        /// This test navigates to the Manage Account page that is part of the Client ID sub-menu on the OneTouch website and changes several 
        /// options through the first two pages
        /// and then unselects those same options, and then verifies the new Terms and Condition's process is working correctly, i.e. the date agreed upon is 
        /// displaying and with the correct values
        /// </summary>
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

        /// <summary>
        /// This test verifies that the Logout button that is part of the Client ID sub-menu functions as expected and that
        /// the client is redirected to the initial login page after selecting the Logout button
        /// </summary>
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
