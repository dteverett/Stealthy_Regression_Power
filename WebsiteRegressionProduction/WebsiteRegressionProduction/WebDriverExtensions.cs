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
    public static class WebDriverExtensions
    {
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

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return driver.FindElements(by);
        }

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

        public static string CaptureBatchNumberExternallyStatements(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
            string captureText = driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchNumberItem"), 5).Text;
            string pattern = @"S\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

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

        public static string CaptureBatchNumberStatements(this IWebDriver driver)
        {

            // Regular Expression to find batch name from WITHIN claims page.  
            string captureText = driver.FindElement(By.Id("ctl00_BreadCrumbContent_TrackBreadCrumb_BatchNumButton"), 5).Text;
            string pattern = @"S\d{10}\w{3}";
            string batch = Regex.Match(captureText, pattern).ToString();

            return batch;
        }

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