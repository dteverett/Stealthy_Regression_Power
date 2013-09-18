using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;
using VendorUploadService.ServiceReference1;

namespace VendorUploadService
{
    /// <summary>
    /// Object that is returned from the VendorUploadService calls.  Not all properties hold values everytime Results is used but have been part
    /// of various aspects of testing throughout the development lifecycle.  For example, timeToRespond was part of the performance evaluation when
    /// that was a serious concern.  Now nothing currently checks or uses the value of that property, but it is still assigned in most calls used in
    /// VendorUploadService.UploadService
    /// </summary>
    public class Results
    {
        public ClaimResult[] claimResults { get; set; }     //The results returned by the Vendor API as called through the reference ServiceReference1
        public DateTime whenUploaded { get; set; }      
        public bool noErrors { get; set; }                  //Trips if there are any validation errors returned in the claimResults
        public Client client { get; set; }                  //Client who's credentials were used to upload the batch
        public Document document { get; set; }              //The batch that was upload
        public TimeSpan timeToRespond { get; set; }         //Legacy
        public List<Exception> Exceptions { get; set; }     //Exceptions returned by Vendor Service or thrown inside VendorUploadService.UploadService
        public bool thrownException { get; set; }           //Trips if any exceptions are thrown

        public Results()
        {
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, IPackage package)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            client = package.Client;
            document = package.Document;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, IPackage package, TimeSpan timeToRespond)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            //this.noErrors = checkBatchForErrors();
            this.client = package.Client;
            this.document = package.Document;
            this.timeToRespond = timeToRespond;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, Document document, Client client)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            this.client = client;
            this.document = document;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, Document document, Client client, TimeSpan timeToRespond)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            //this.noErrors = checkBatchForErrors();
            this.client = client;
            this.document = document;
            this.timeToRespond = timeToRespond;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public static bool CheckBatchForErrors(ClaimResult[] claimResults)
        {
            int errorCount = 0;
            bool noErrors;
            if (claimResults != null && claimResults.Length > 0)
            {
                foreach (var claim in claimResults)
                {
                    errorCount += claim.Errors.Length;
                }
            }
            else
                errorCount = 99999;
            if (errorCount == 0)
                noErrors = true;
            else
                noErrors = false;
            return noErrors;
        }
    }

    /// <summary>
    /// Object used when the vendor service DeleteClaims method is called, as defined in the service reference ServiceReference1
    /// 
    /// </summary>
    public class DeletionResults
    {
        public ClaimDeletionStatus[] claimDeletionStatuses { get; set; }  //Allows VendorUploadService.UploadService.Delete to call the vendor API delete multiple times and return an array of results instead of just 1
        public ClaimDeletionStatus claimDeletionStatus { get; set; }    //typical results as the Vendor API only accepts 1 claim ID per call
        public DateTime whenUploaded { get; set; }
        public Client client { get; set; }
        public Document document { get; set; }
        public TimeSpan timeToRespond { get; set; }
        public List<Exception> Exceptions { get; set; }
        public bool thrownException { get; set; }

        public DeletionResults()
        {
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public DeletionResults(ClaimDeletionStatus[] claimDeletionStatuses, IPackage package)
        {
            this.claimDeletionStatuses = claimDeletionStatuses;
            this.client = package.Client;
            this.document = package.Document;
            this.whenUploaded = DateTime.Now;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public DeletionResults(ClaimDeletionStatus[] claimDeletionStatuses, IPackage package, TimeSpan timeToRespond)
        {
            this.claimDeletionStatuses = claimDeletionStatuses;
            this.client = package.Client;
            this.document = package.Document;
            this.whenUploaded = DateTime.Now;
            this.timeToRespond = timeToRespond;
            thrownException = false;
            Exceptions = new List<Exception>();
        }
    }

    /// <summary>
    /// Object returned from methods that call the vendor API getValidation methods as defined in the service reference ServiceReference1
    /// </summary>
    public class ValidationResults
    {
        public ApexValidationResponse ClaimValidationResponse { get; set; }
        public ApexValidationResponse[] MultiClaimValidationResponses { get; set; }
        public DateTime whenUploaded { get; set; }
        public TimeSpan timeToRespond { get; set; }
        public List<Exception> Exceptions { get; set; }
        public bool thrownException { get; set; }

        public ValidationResults()
        {
            thrownException = false;
            Exceptions = new List<Exception>();
        }
    }
}
