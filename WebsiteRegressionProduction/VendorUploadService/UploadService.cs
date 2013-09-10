using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VendorUploadService.ServiceReference1;
using TestLibrary;

namespace VendorUploadService
{
    public static class UploadService
    {
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

                    //var r = uploader.ImportClaim(package.Client.Username, package.Client.Password,
                    //    Encoding.ASCII.GetBytes(batch));

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

                    //var r = uploader.ImportClaim(package.Client.Username, package.Client.Password,
                    //    Encoding.ASCII.GetBytes(batch));

                    sw.Stop();
                    result.timeToRespond = sw.Elapsed;
                    result.client = package.Client;
                    result.document = package.Document;
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

        public static ValidationResults GetMultiValidationErrors(Client client, string[] vendorClaims)
        {
            Stopwatch sw =  new Stopwatch();
            var uploader = new ServiceReference1.ClaimImportServiceClient();
            uploader.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
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
