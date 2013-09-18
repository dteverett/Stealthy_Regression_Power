using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;


namespace WebsiteRegressionProduction
{
    /// <summary>
    /// Webdriver extensions for the Selenium Libraries.
    /// Most extensions either deal with adding extra time for the command to execute to accomidate the time it often takes
    /// pages to load, or specific to Apex Processes, i.e. FindBatchNumberClaims()
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Webdriver extensions that finds an element and adds a timeout argument that allows for time it takes webpages to load
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(drv => drv.FindElement(by));
                }
                catch (Exception)
                {
                    try
                    {
                        return wait.Until(drv => drv.FindElement(by));
                    }
                    catch (Exception)
                    {
                        //TODO
                        //Add logging
                    }

                }
            }
            return driver.FindElement(by);
        }

        /// <summary>
        /// Webdriver extensions that finds mutliple elements and adds a timeout argument that allows for time it takes webpages to load
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return driver.FindElements(by);
        }

        /// <summary>
        /// Webdriver extension that returns a bool for whether or not an element can be located.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns>bool</returns>
        public static bool isElementPresent(this IWebDriver driver, By by)
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

        /// <summary>
        /// Webdriver extension that returns a bool for whether or not an element can be located.
        ///  allows for the parameter timeoutInSeconds
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns>bool</returns>
        public static bool isElementPresent(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds <= 0)
                timeoutInSeconds = 5;
            try
            {
                driver.FindElement(by, timeoutInSeconds);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Driver extension specific to Apex processes in Production.  Looks for the top listed batch number on the batch listing page
        /// Specific to Statements and the Statement Batch Listing Page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>string: batch number</returns>
        public static string CaptureBatchNumberExternallyStatements(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
            string captureText = driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchNumberItem"), 5).Text;
            string pattern = @"S\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

        /// <summary>
        /// Driver extension specific to Apex processes in Production.  Looks for the top listed batch number on the batch listing page
        /// Specific to claims and the Claims Batch listing page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string CaptureBatchNumberExternallyClaims(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
            string captureText = "";
            if (driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchNumberItem")))
            {
                 captureText =
                    driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchNumberItem"), 5).Text;
            }
            else if (driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_lblBatchNumber")))
            {
                 captureText =
                    driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_lblBatchNumber")).Text;
            }
            string pattern = @"\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

        /// <summary>
        /// Captures the batch number of statements from within a Statement Form Edit Page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string CaptureBatchNumberStatements(this IWebDriver driver)
        {

            // Regular Expression to find batch name from WITHIN claims page.  
            string captureText = driver.FindElement(By.Id("ctl00_BreadCrumbContent_TrackBreadCrumb_BatchNumButton"), 5).Text;
            string pattern = @"S\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

        /// <summary>
        /// Captures the batch number of claims from within a claim form edit page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string CaptureBatchNumberClaims(this IWebDriver driver)
        {
            // Regular Expression to find batch name from WITHIN claims page.  
            string captureText = driver.FindElement(By.Id("prevNextClaimCtrl_hlBatch"), 5).Text;
            string pattern = @"\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

    }
}