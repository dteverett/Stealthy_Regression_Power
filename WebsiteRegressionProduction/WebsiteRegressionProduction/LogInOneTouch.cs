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
    [TestFixture]
    public class LogInOneTouch : Test
    {
        private IWebDriver driver;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://onetouch.apexedi.com/";
            verificationErrors = new StringBuilder();
            
            SetupTestGeneric();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            TearDownTestGeneric(); 
        }

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
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Eligibility[\\s\\S]*$"));
            
            endOfTest();
        }

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
            // Warning: verifyTextPresent may require manual changes
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
