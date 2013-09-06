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
    public class Results
    {
        public ClaimResult[] claimResults { get; set; }
        public DateTime whenUploaded { get; set; }
        public bool noErrors { get; set; }
        public IPackage package { get; set; }
        public TimeSpan timeToRespond { get; set; }
        public List<Exception> Exceptions { get; set; }
        public bool thrownException { get; set; }

        public Results()
        {
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, IPackage package)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            //this.noErrors = checkBatchForErrors();
            this.package = package;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public Results(ClaimResult[] claimResults, IPackage package, TimeSpan timeToRespond)
        {
            this.claimResults = claimResults;
            this.whenUploaded = DateTime.Now;
            //this.noErrors = checkBatchForErrors();
            this.package = package;
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

    public class DeletionResults
    {
        public ClaimDeletionStatus[] claimDeletionStatuses { get; set; }
        public ClaimDeletionStatus claimDeletionStatus { get; set; }
        public DateTime whenUploaded { get; set; }
        public IPackage package { get; set; }
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
            this.package = package;
            this.whenUploaded = DateTime.Now;
            thrownException = false;
            Exceptions = new List<Exception>();
        }

        public DeletionResults(ClaimDeletionStatus[] claimDeletionStatuses, IPackage package, TimeSpan timeToRespond)
        {
            this.claimDeletionStatuses = claimDeletionStatuses;
            this.package = package;
            this.whenUploaded = DateTime.Now;
            this.timeToRespond = timeToRespond;
            thrownException = false;
            Exceptions = new List<Exception>();
        }
    }

    public class ValidationResults
    {
        public ApexValidationResponse ClaimValidationResponse { get; set; }
        public ApexValidationResponse[] MultiClaimValidationResponses { get; set; }
        //public ApexValidationResponse[][] arrayOfMultiClaimValidationResponses { get; set; }


        public DateTime whenUploaded { get; set; }
        public IPackage package { get; set; }
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
