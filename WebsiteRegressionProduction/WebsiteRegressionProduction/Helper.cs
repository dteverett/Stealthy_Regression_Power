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


namespace WebsiteRegressionProduction
{
    /// <summary>
    /// Helper class contains all methods used for calling external resources.  This includes calls to the databases, as well as 
    /// calls to the OneTouch upload project
    /// </summary>
    public static class Helper
    {
        const string defaultLogin = "admin";
        const string defaultPassword = "hedge1!";
        const string baseURL = @"http:\\onetouch.apexedi.com";

        /// <summary>
        /// Login logs the driver into OneTouch with the credentials associated with the client parameter passed in
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="client"></param>
        /// <returns>bool of whether or not the login is successful</returns>
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

        /// <summary>
        /// Calls the OneTouch upload
        /// </summary>
        /// <param name="package"></param>
        /// <returns>bool of successful upload</returns>
        public static bool UploadBatch(OneTouchUploadPackage package)
        {
            bool result = CallUploadService.upload(package);
            return result;
        }

        /// <summary>
        /// DeleteBatch is used to keep test data from production data and to ensure no test claims are processed and sent
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="package"></param>
        public static void DeleteBatch(string batch, OneTouchUploadPackage package)
        {
            DatabaseCalls.DeleteBatch(batch, package.Document.DocType);
        }

        /// <summary>
        /// Test batches often upload as duplicates because the tests have been run multiple times using the same data.  Duplicates cannot be editted and worked with
        /// so this method will call DatabaseCalls from testlibrary and update the batch status to ready on the batch that is passed as a parameter
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="package"></param>
        public static void UpdateDuplicates(string batch, OneTouchUploadPackage package)
        {
            DatabaseCalls.UpdateDuplicates(batch, package.Document.DocType);
        }
        
        /// <summary>
        /// This method mocks the typical Apex processes of getting the batch from the upload directory to the auto import and then 5010 directory.  Because this process
        /// is unreliable and can often take a long time, and because it is not this process but the website that is tested, this method bypasses this and automatically
        /// moves the batch from the upload directory to the base:claimstaker\claims\auto\5010 directory, if the batch can be found in uploads.  If the batch cannot
        /// be found in the upload directory, the method then looks in the base:claimstaker\claims\auto directory, and if it finds the batch there it will automatically move
        /// the batch to the base:claimstaker\claims\auto\5010 directory
        /// </summary>
        /// <param name="batch"></param>
        /// <returns>If batch can be found, returns bool:True, else bool:False</returns>
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

        /// <summary>
        /// CreateBatchRegex takes a batch number as stored in the database and returns a batch number as stored in the directory structure, 
        /// i.e. 1234567890ZZZ becomes 1234567890.ZZZ
        /// </summary>
        /// <param name="batch"></param>
        /// <returns>string representation of the batch number/name</returns>
        private static string createBatchRegex(string batch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(batch.Substring(0, 10));
            sb.Append(@"\.");
            sb.Append(batch.Substring(10));

            return sb.ToString();
        }

        /// <summary>
        /// Seems to be a duplicate of createBatchRegex, will need further investigating.  Only use is in Helper class Process5010claims
        /// </summary>
        /// <param name="batch"></param>
        /// <returns>string representation of the batch number/name</returns>
        private static string addDecimalToBatch(string batch)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(batch.Substring(0, 10));
            sb.Append(".");
            sb.Append(batch.Substring(10));

            return sb.ToString();
        }

        /// <summary>
        /// Calls TestLibrary.UpdateProcessedToReady to change all claim's statuses to Ready.  
        /// Purpose: If a batch is uploaded to an account that has the Claimstaker.Provider_T.isTestAccount_BT set to true (test account), and those claims do not fail validations,
        /// those claims will not have a status of Ready as would be typical of non-test accounts, but of Processed, to help ensure no test batches are sent to insurance payers.
        /// However, claims in a Processed status cannot be editted by the client
        /// </summary>
        /// <param name="package"></param>
        /// <param name="batch"></param>
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
