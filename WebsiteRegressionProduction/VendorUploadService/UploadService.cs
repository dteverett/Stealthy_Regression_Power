using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Text;
using VendorUploadService.ServiceReference1;
using TestLibrary;

namespace VendorUploadService
{
    /// <summary>
    /// Calls to the vendor upload service
    /// </summary>
    public static class UploadService
    {
        /// <summary>
        /// Uploads to the vendor API as configured in the Service Reference currently configured as: ServiceReference1
        /// </summary>
        /// <param name="client"></param>
        /// <param name="document"></param>
        /// <returns>Object Results as defined in the VendorUploadService project </returns>
        public static Results CallUploadService(Client client, Document document)
        {
            string batch = File.ReadAllText(document.Path);
            Results result = new Results();
            using (var uploader = new ServiceReference1.ClaimImportServiceClient())
            {
                uploader.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                uploader.Open();
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    result.claimResults = uploader.ImportClaim(client.Username, client.Password,
                        Encoding.ASCII.GetBytes(batch));
                    sw.Stop();
                    result.timeToRespond = sw.Elapsed;
                    result.client = client;
                    result.document = document;
                    result.whenUploaded = DateTime.Now;
                    if (result.claimResults.Length < 1) //need to compare this another way, doesn't come back as null
                    {
                        throw new NullReturnedValueException();
                    }
                    result.noErrors = Results.CheckBatchForErrors(result.claimResults);
                }
                catch (FaultException e)
                {
                    result.Exceptions.Add(e);
                    result.thrownException = true;
                }
                catch (NullReturnedValueException e)
                {
                    result.Exceptions.Add(e);
                    result.thrownException = true;
                }
                catch (Exception e)
                {
                    result.thrownException = true;
                    result.Exceptions.Add(e);
                }
            }
            return result;
        }

        /// <summary>
        /// Calls vendor upload service with only a package as a parameter.  IPackage is now legacy and is defined in TestLibrary
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static Results CallUploadService(IPackage package)
        {
            string batch = File.ReadAllText(package.Document.Path);
            Results result = new Results();
            using (var uploader = new ServiceReference1.ClaimImportServiceClient())
            {
                uploader.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                uploader.Open();
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    result.claimResults = uploader.ImportClaim(package.Client.Username, package.Client.Password,
                        Encoding.ASCII.GetBytes(batch));
                    sw.Stop();
                    result.timeToRespond = sw.Elapsed;
                    result.client = package.Client;
                    result.document = package.Document;
                    result.whenUploaded = DateTime.Now;
                    if (result.claimResults.Length < 1) //Vendor API often will only return a zero-length array when one might expect an error.  This is to ensure we know when a zero-length array is returned
                    {
                        throw new NullReturnedValueException();
                    }
                    result.noErrors = Results.CheckBatchForErrors(result.claimResults);
                }
                catch (FaultException e)
                {
                    result.Exceptions.Add(e);
                    result.thrownException = true;
                }
                catch (NullReturnedValueException e)
                {
                    result.Exceptions.Add(e);
                    result.thrownException = true;
                }
                catch (Exception e)
                {
                    result.thrownException = true;
                    result.Exceptions.Add(e);
                }
            }
            return result;
        }

        /// <summary>
        /// Calls the Vendor API DeleteClaim as defined in the Service Reference ServiceReference1
        /// Uses VendorPackage which inherits from IPackage
        /// Currently porting all calls to method that takes Client instead of package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="vendorClaimID"></param>
        /// <returns></returns>
        public static DeletionResults CallDeleteClaimService(VendorPackage package, string[] vendorClaimID)
        {
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            var results = new DeletionResults();
            ClaimDeletionStatus[] returned = new ClaimDeletionStatus[vendorClaimID.Length];
            for (int i = 0; i < returned.Length; ++i)
            {
                returned[i] = ClaimDeletionStatus.ClaimNotFound;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                for (int i = 0; i < vendorClaimID.Length; i++)
                {
                    returned[i] = uploader.DeleteClaim(package.Client.Username, package.Client.Password, vendorClaimID[i]);
                }
            }
            catch (Exception e)
            {
                results.Exceptions.Add(e);
                results.thrownException = true;
            }
            sw.Stop();
            results.timeToRespond = sw.Elapsed;
            results.whenUploaded = DateTime.Now;
            results.client = package.Client;
            results.document = package.Document;
            results.claimDeletionStatuses = returned;

            return results;
        }

        /// <summary>
        /// Calls the DeleteClaim method as defined in the service reference ServiceReference1
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vendorClaimID"></param>
        /// <returns>DeletionResults as defined in VendorUploadService.Results</returns>
        public static DeletionResults CallDeleteClaimService(Client client, string vendorClaimID)
        {
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            var results = new DeletionResults();
            ClaimDeletionStatus returned = new ClaimDeletionStatus();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                returned = uploader.DeleteClaim(client.Username, client.Password, vendorClaimID);
            }
            catch (Exception e)
            {
                results.Exceptions.Add(e);
                results.thrownException = true;
            }
            sw.Stop();
            results.timeToRespond = sw.Elapsed;
            results.whenUploaded = DateTime.Now;
            results.claimDeletionStatus = returned;
            return results;
        }

        /// <summary>
        /// Calls the getValidationMessages method that takes one single claim ID, as defined in the service reference ServiceReference1
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vendorClaimID"></param>
        /// <returns>ValidationResults as defined in VendorUploadService.Results</returns>
        public static ValidationResults GetValidationErrors(Client client, string vendorClaimID)
        {
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            var results = new ValidationResults();
            Stopwatch sw = new Stopwatch();
            sw.Start();
                try
                {
                    results.ClaimValidationResponse = uploader.GetMedicalValidationMessages(client.Username,
                        client.Password,
                        vendorClaimID);
                }
                catch (Exception e)
                {
                    results.Exceptions.Add(e);
                    results.thrownException = true;
                }
            sw.Stop();
            results.timeToRespond = sw.Elapsed;
            results.whenUploaded = DateTime.Now;
            return results;
        }

        /// <summary>
        /// Calls the GetMultipleMedicalValidationMessages method as defined in the service reference ServiceReference1
        /// which takes an array of strings as it's parameter, as well as a username and password for authentication purposes
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vendorClaims"></param>
        /// <returns>ValidationResults as defined in VendorUploadService.Results</returns>
        public static ValidationResults GetMultiValidationErrors(Client client, string[] vendorClaims)
        {
            Stopwatch sw =  new Stopwatch();
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            //uploader.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);  //Set previous to claim limit being set @ 20 to prevent timeouts
            uploader.Open();
            var results = new ValidationResults();
            sw.Start();
            try
            {
                results.MultiClaimValidationResponses = uploader.GetMultipleMedicalValidationMessages(client.Username,
                    client.Password, vendorClaims);
                sw.Stop();
            }
            catch (Exception e)
            {
                results.Exceptions.Add(e);
                results.thrownException = true;
            }
            uploader.Close();
            results.whenUploaded = DateTime.Now;
            results.timeToRespond = sw.Elapsed;
            return results;
        }

        /// <summary>
        /// Legacy, no longer needed.  Previous to requirements being further defined, any number of claims were allowed to upload, but on the client side of
        /// wvc it would timeout waiting for all those validations.  This test and GetMultiValidationErrors were both built previous to requirements update
        /// As currently stands, the client cannot be expected or asked to make any extra changes in their code, so the limit was set to 20 claims, so the 
        /// timout problem is now moot.  
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vendorClaims"></param>
        /// <returns></returns>
        public static ValidationResults GetMultipleValidationResultsWithoutChanges(Client client, string[] vendorClaims)
        {
            Stopwatch sw = new Stopwatch();
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            var results = new ValidationResults();
            sw.Start();
            try
            {
                results.MultiClaimValidationResponses = uploader.GetMultipleMedicalValidationMessages(client.Username,
                    client.Password, vendorClaims);
                sw.Stop();
            }
            catch (Exception e)
            {
                results.Exceptions.Add(e);
                results.thrownException = true;
            }
            uploader.Close();
            results.whenUploaded = DateTime.Now;
            results.timeToRespond = sw.Elapsed;
            return results;
        }

        /// <summary>
        /// Built solely for testing purposes; initial build of project did not adhere to the requirements that multiple claim IDs could be passed as an argument
        /// to getValidationErrors.  For the first couple weeks of testing, only one claim ID could be sent at a time.  To expidite processes on the testing end,
        /// this method was designed to take a single batch number, calls the database and gets an array of all Claim IDs associated with that batch number,
        /// and then calls the GetMedicalValidationMessages multiple times, one for each element in the array.  
        /// Since project was updated to fully meet requirements, this method is legacy
        /// This method makes reference to a non-production db as the Vendor API does not exist in production.  If needed, to switch this method to production
        /// database calls, comment out the line where claimsInfo is declared and initialized, and uncomment out the line directly below it
        /// </summary>
        /// <param name="client"></param>
        /// <param name="batchNum"></param>
        /// <returns></returns>
        public static ValidationResults GetMultiValidationErrors(Client client, string batchNum)
        {
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            uploader.InnerChannel.OperationTimeout = new TimeSpan(0 , 10, 0);
            uploader.Open();
            var results = new ValidationResults();
            var claimsInfo = TestDatabaseCalls.GetClaimMedicalIDs(batchNum);                                // TEST REFERENCE -- NEEDS TO BE ALTERED BEFORE RELEASE TO PRODUCTION TEST SUITES
            //var claimsInfo = DatabaseCalls.GetClaimMedicalIDs(batchNum);                                  //PRODUCTION REFERENCE
            results.MultiClaimValidationResponses = new ApexValidationResponse[claimsInfo.Count];
            //results.claimValidationErrors = new ApexValidationResponse[claimsInfo.Count * 2];
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; ++i)
            {
                results.MultiClaimValidationResponses[i] = uploader.GetMedicalValidationMessages(client.Username,
                    client.Password, claimsInfo.Count < results.MultiClaimValidationResponses.Length && claimsInfo.Count <= i ? claimsInfo[i - claimsInfo.Count] : claimsInfo[i]);
            }
            uploader.Close();
            return results;
        }
    }
}
