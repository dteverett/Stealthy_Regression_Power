using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OneTouchUploadProduction;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TestLibrary;
//using VendorUploadService;


namespace WebsiteRegressionProduction_InternetExplorer
{
    public static class Helper
    {
        const string defaultLogin = "admin";
        const string defaultPassword = "hedge1!";
        const string baseURL = @"http:\\onetouch.apexedi.com";

        public static bool Login(this IWebDriver driver, Client client)
        {
            driver.Navigate().GoToUrl(baseURL + "/secure/Login.aspx?redir=%2fsecure%2fDefault.aspx");
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername"), 15).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).SendKeys(client.Username);
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).SendKeys(client.Password == null ? defaultPassword : client.Password);
            driver.FindElement(By.Id("ctl00_MainContent_btnSubmit")).Click();


            if (driver.isElementPresent(By.Id("track_link"), 10))
            {
                return true;
            }

            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbUsername")).SendKeys(defaultLogin + client.ClientID);
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_tbPassword")).SendKeys(defaultPassword);
            driver.FindElement(By.Id("ctl00_MainContent_btnSubmit")).Click();
            if (driver.isElementPresent(By.Id("track_link"), 10))
            {
                return true;
            }
            return false;
        }

        public static bool UploadBatch(OneTouchUploadPackage package)
        {
            bool result = CallUploadService.upload(package);
            return result;
        }

        public static void DeleteBatch(string batch, OneTouchUploadPackage package)
        {
            DatabaseCalls.DeleteBatch(batch, package.Document.DocType);
        }



        public static void UpdateDuplicates(string batch, OneTouchUploadPackage package)
        {
            DatabaseCalls.UpdateDuplicates(batch, package.Document.DocType);
        }



        internal static bool Process5010Claims(string batch)
        {
            string batchRegex = createBatchRegex(batch);
            string batchOnFileSystem = addDecimalToBatch(batch);
            bool isFound = false;
            string pth = @"\\hedgefrog\root\uploads";
            string destination = @"\\apexdata\data\claimstaker\claims\auto\5010";
            string[] files = Directory.GetFiles(pth);
            foreach (var file in files)
            {
                if (Regex.IsMatch(file, batchRegex))
                {
                    string fullPath = Path.Combine(pth, batchOnFileSystem);
                    string destFullPath = Path.Combine(destination, batchOnFileSystem);
                    try
                    {
                        File.Move(fullPath, destFullPath);
                        isFound = true;
                        break;
                    }
                    catch (FileNotFoundException)
                    { // Presumably the file has just been grabbed by apexwatcher and is being moved
                        isFound = false;
                        break;
                    } 
                    catch (IOException)
                    {
                        isFound = false;
                        break;
                    }//Already been moved to the new directory
                }
            }

            if (!isFound)
            {
                string newPth = @"\\apexdata\data\claimstaker\claims\auto";
                string[] newFiles = Directory.GetFiles(newPth);
                foreach (var file in newFiles)
                {
                    if (Regex.IsMatch(file, batchRegex))
                    {
                        string fullPath = Path.Combine(newPth, batchOnFileSystem);
                        string destFullPath = Path.Combine(destination, batchOnFileSystem);
                        try
                        {
                            File.Move(fullPath, destFullPath);
                            isFound = true;
                            break;
                        }
                        catch (FileNotFoundException)
                        {
                            break;
                        } //File is already being processed, ignore
                        catch (IOException)
                        {
                            break;
                        }//Already been moved to the new directory
                    }
                }
            }

            return isFound;
        }

        private static string createBatchRegex(string batch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(batch.Substring(0, 10));
            sb.Append(@"\.");
            sb.Append(batch.Substring(10));

            return sb.ToString();
        }

        private static string addDecimalToBatch(string batch)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(batch.Substring(0, 10));
            sb.Append(".");
            sb.Append(batch.Substring(10));

            return sb.ToString();
        }

        internal static void UpdateProcessedToReady(OneTouchUploadPackage package, string batch)
        {
            DatabaseCalls.UpdateProcessed(package, batch);
        }

        public static void UpdateDuplicateToFailed(OneTouchUploadPackage package, string batch)
        {
            DatabaseCalls.UpdateDuplicateToFailed(package, batch);
        }

        internal static void UpdateBatchToActive(string batch)
        {
            DatabaseCalls.UpdateBatchToActive(batch);
        }

        public static void EliminateTestAccountFailedClaims()
        {
            DatabaseCalls.EliminateTestAccountFailedClaims();
        }


        internal static void UpdateReadyToProcessed(string batch)
        {
            DatabaseCalls.UpdateReadyToProcessed(batch);
        }
    }
}
