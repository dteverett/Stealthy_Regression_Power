using System;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace WebsiteRegressionProduction
{
    /// <summary>
    /// Class holds all tests dealing with the onetouch login page
    /// </summary>
    [TestFixture]
    public class LogInOneTouch : Test
    {
        private IWebDriver driver;
        private string baseURL;
        private bool acceptNextAlert = true;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://onetouch.apexedi.com/";
            verificationErrors = new StringBuilder();
            
            SetupTestGeneric();
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
            TearDownTestGeneric(); 
        }

        /// <summary>
        /// Tests that a successful login is the result of using a valid username/password combination
        /// </summary>
        [Test]
        public void TheLogInOneTouchTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.Navigate().GoToUrl(baseURL + "/secure/Login.aspx?redir=%2fsecure%2fDefault.aspx");
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).SendKeys("demo1");
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).SendKeys("medical1");
            driver.FindElement(By.Id("ctl00_MainContent_btnSubmit")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Eligibility[\\s\\S]*$"));
            
            endOfTest();
        }

        /// <summary>
        /// Attempts to logon to the baseURL using the demo2 username and the demo1 password and verifies that the login unsuccessful dialog is found
        /// on the subsequent page
        /// </summary>
        [Test]
        public void TheMisMatchedLoginCredentialsTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.Navigate().GoToUrl(baseURL + "/secure/Login.aspx?redir=%2fsecure%2fDefault.aspx");
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).SendKeys("demo2");
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).SendKeys("medical1");
            driver.FindElement(By.Id("ctl00_MainContent_btnSubmit")).Click();
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Login Unsuccessful[\\s\\S]*$"));
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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alert.Text;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
