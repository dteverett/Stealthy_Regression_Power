using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;


namespace TestLibrary
{
    public static class DatabaseCalls
    {
        public static void DeleteBatch(string batch, DocumentType type)
        {
            using (Connection connection = new Connection())
            {
                if (type == DocumentType.MedicalClaim)
                {
                    var deleteBatch = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in deleteBatch)
                    {
                        claim.ClaimStatusType_ID = 2;
                    }

                    var unProcessed =
                        connection._repository.ClaimMedicalBase_Ts.Where(
                            x => x.ClientID_VC == "ZZZ" && x.ClaimStatusType_ID == 1);
                    foreach (var claim in unProcessed)
                    {
                        claim.ClaimStatusType_ID = 2;
                    }
                    connection._repository.SubmitChanges();
                }
                else if (type == DocumentType.DentalClaim)
                {
                    var deleteBatch = connection._repository.ClaimDentalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in deleteBatch)
                    {
                        claim.ClaimStatusType_ID = 2;
                    }

                    var unProcessed =
                        connection._repository.ClaimDentalBase_Ts.Where(
                            x => x.ClientID_VC == "ZZD" && x.ClaimStatusType_ID == 1);
                    foreach (var claim in unProcessed)
                    {
                        claim.ClaimStatusType_ID = 2;
                    }
                    connection._repository.SubmitChanges();
                }
                else if (type == DocumentType.Statement)
                {
                    var deleteBatch = connection._repository.Statement_Ts.Where(x => x.BatchNum_VC == batch);
                    foreach (var statement in deleteBatch)
                    {
                        statement.ClaimStatus_TI = 2;
                    }
                }


                var btch = connection._repository.Batch_Ts.FirstOrDefault(x => x.BatchName_VC == batch);
                var btchID = btch.Batch_ID;
                var btchLggr = connection._repository.BatchEventLogMessage_Ts.FirstOrDefault(x => x.Batch_ID == btchID);
                connection._repository.BatchEventLogMessage_Ts.DeleteOnSubmit(btchLggr);
                connection._repository.Batch_Ts.DeleteOnSubmit(btch);
                connection._repository.SubmitChanges();
            }
        }

        public static void UpdateDuplicates(string batch, DocumentType type)
        {
            using (Connection connection = new Connection())
            {
                if (type == DocumentType.MedicalClaim)
                {
                    var claims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);

                    foreach (var claim in claims)
                    {
                        claim.ClaimStatusType_ID = 1;
                    }

                }
                else if (type == DocumentType.DentalClaim)
                {
                    var claims = connection._repository.ClaimDentalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in claims)
                    {
                        claim.ClaimStatusType_ID = 1;
                    }
                }
                else if (type == DocumentType.Statement)
                {
                    var claims = connection._repository.Statement_Ts.Where(x => x.BatchNum_VC == batch);
                    foreach (var claim in claims)
                    {
                        claim.ClaimStatus_TI = 4;
                        claim.IsDuplicateClaim_BT = false;
                    }
                }
                connection._repository.SubmitChanges();
            }
        }

        public static void UpdateProcessed(IPackage package, string batch)
        {
            using (Connection connection = new Connection())
            {
                if (package.Document.DocType == DocumentType.MedicalClaim)
                {
                    var claims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in claims)
                    {
                        claim.ClaimStatusType_ID = 1;
                    }
                }
                else if (package.Document.DocType == DocumentType.DentalClaim)
                {
                    var claims = connection._repository.ClaimDentalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in claims)
                    {
                        claim.ClaimStatusType_ID = 1;
                    }
                }
                else if (package.Document.DocType == DocumentType.Statement)
                {
                    var claims = connection._repository.Statement_Ts.Where(x => x.BatchNum_VC == batch);
                    foreach (var claim in claims)
                    {
                        claim.ClaimStatus_TI = 4;
                    }
                }
                connection._repository.SubmitChanges();
            }

        }

        public static void UpdateDuplicateToFailed(IPackage package, string batch)
        {
            using (Connection connection = new Connection())
            {
                if (package.Document.DocType == DocumentType.MedicalClaim)
                {
                    var claims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in claims)
                    {
                        if (claim.ClaimStatusType_ID == 6)
                        {
                            claim.ClaimStatusType_ID = 3;
                        }
                    }
                }
                else if (package.Document.DocType == DocumentType.DentalClaim)
                {
                    var claims = connection._repository.ClaimDentalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in claims)
                    {
                        if (claim.ClaimStatusType_ID == 6)
                        {
                            claim.ClaimStatusType_ID = 3;
                        }
                    }
                }
                connection._repository.SubmitChanges();
            }
        }

        public static void UpdateBatchToActive(string batch)
        {
            using (Connection connection = new Connection())
            {
                var btch = connection._repository.Batch_Ts.FirstOrDefault(x => x.BatchName_VC == batch);
                btch.BatchStatus_ID = 4;
                connection._repository.SubmitChanges();
            }
        }

        internal static TestUserCredentials_T GetLogonCredentials(string clientID)
        {
            using (Connection connection = new Connection())
            {
                var creds = connection._repository.TestUserCredentials_Ts.FirstOrDefault(x => x.ClientID_VC == clientID);
                return creds;
            }
        }

        public static void EliminateTestAccountFailedClaims()
        {
            using (Connection connection = new Connection())
            {
                var failedTestClaims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.ClientID_VC == "ZZZ");
                foreach (var claim in failedTestClaims)
                {
                    claim.ClaimStatusType_ID = 2;
                }
                connection._repository.SubmitChanges();
            }
        }

        public static void CleanUpVendorBatch(string[] claimsToDelete)
        {
            using (Connection connection = new Connection())
            {
                var claimInfo =
                    connection._repository.ClaimMedicalClaimInformation_Ts.FirstOrDefault(
                        x => x.ValueAddedNetworkTraceNumber_VC == claimsToDelete[0]);
                if (claimInfo == null)
                {
                    var baseInfo =
                        connection._repository.ClaimMedicalBase_Ts.Where(x => x.ClientID_VC == "ZZZ")
                            .OrderByDescending(x => x.ReportDate_DT)
                            .FirstOrDefault();
                    var batch = baseInfo.BatchNumber_VC;
                    var batchClaims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in batchClaims)
                    {
                        connection._repository.ClaimMedicalBase_Ts.DeleteOnSubmit(claim);
                    }
                }
                else
                {
                    var claimMedicalBase_ID = claimInfo.ClaimMedicalBase_ID;
                    var claimBaseInfo =
                        connection._repository.ClaimMedicalBase_Ts.FirstOrDefault(
                            x => x.ClaimMedicalBase_ID == claimMedicalBase_ID);
                    var batch = claimBaseInfo.BatchNumber_VC;
                    var batchClaims = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in batchClaims)
                    {
                        connection._repository.ClaimMedicalBase_Ts.DeleteOnSubmit(claim);
                    }
                }
                connection._repository.SubmitChanges();
            }
        }

        public static Queue<string> GetClaimMedicalIDs(string batchNum)
        {
            using (Connection connection = new Connection())
            {
                var vendorIDs = new Queue<string>();
                var claimIDs =
                    connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batchNum)
                        .Select(x => x.ClaimMedicalBase_ID);
                var results =
                    connection._repository.ClaimMedicalClaimInformation_Ts.Where(
                        x => claimIDs.Contains(x.ClaimMedicalBase_ID));
                foreach (var result in results)
                {
                    vendorIDs.Enqueue(result.ValueAddedNetworkTraceNumber_VC);
                }
                return vendorIDs;
            }
        }

        public static void UpdateReadyToProcessed(string batch)
        {
            using (Connection connection = new Connection())
            {
                var rows = connection._repository.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                foreach (var claim in rows)
                {
                    claim.ClaimStatusType_ID = 2;
                }
                connection._repository.SubmitChanges();
            }
        }
    }
}
