using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorUploadService
{
    public class TestDatabaseCalls
    {
        public static void CleanUpVendorBatch(string[] claimsToDelete)
        {
            using (TestConnection connection = new TestConnection())
            {
                var claimInfo =
                    connection._testRepos.ClaimMedicalClaimInformation_Ts.FirstOrDefault(
                        x => x.ValueAddedNetworkTraceNumber_VC == claimsToDelete[0]);
                if (claimInfo == null)
                {
                    var baseInfo =
                        connection._testRepos.ClaimMedicalBase_Ts.Where(x => x.ClientID_VC == "ZZZ")
                            .OrderByDescending(x => x.ReportDate_DT)
                            .FirstOrDefault();
                    var batch = baseInfo.BatchNumber_VC;
                    var batchClaims = connection._testRepos.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in batchClaims)
                    {
                        connection._testRepos.ClaimMedicalBase_Ts.DeleteOnSubmit(claim);
                    }
                }
                else
                {
                    var claimMedicalBase_ID = claimInfo.ClaimMedicalBase_ID;
                    var claimBaseInfo =
                        connection._testRepos.ClaimMedicalBase_Ts.FirstOrDefault(
                            x => x.ClaimMedicalBase_ID == claimMedicalBase_ID);
                    var batch = claimBaseInfo.BatchNumber_VC;
                    var batchClaims = connection._testRepos.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batch);
                    foreach (var claim in batchClaims)
                    {
                        connection._testRepos.ClaimMedicalBase_Ts.DeleteOnSubmit(claim);
                    }
                }
                connection._testRepos.SubmitChanges();
            }
        }

        //public static Queue<string> GetClaimMedicalIDs(string batchNum)
        //{
        //    using (TestConnection connection = new TestConnection())
        //    {
        //        var vendorIDs = new Queue<string>();
        //        var claimIDs =
        //            connection._testRepos.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batchNum)
        //                .Select(x => x.ClaimMedicalBase_ID);
        //        var results =
        //            connection._testRepos.ClaimMedicalClaimInformation_Ts.Where(
        //                x => claimIDs.Contains(x.ClaimMedicalBase_ID));
        //        foreach (var result in results)
        //        {
        //            vendorIDs.Enqueue(result.ValueAddedNetworkTraceNumber_VC);
        //        }
        //        return vendorIDs;
        //    }
        //}

        public static List<string> GetClaimMedicalIDs(string batchNum)
        {
            using (TestConnection connection = new TestConnection())
            {
                var vendorIDs = new List<string>();
                var claimIDs =
                    connection._testRepos.ClaimMedicalBase_Ts.Where(x => x.BatchNumber_VC == batchNum)
                        .Select(x => x.ClaimMedicalBase_ID);
                var results =
                    connection._testRepos.ClaimMedicalClaimInformation_Ts.Where(
                        x => claimIDs.Contains(x.ClaimMedicalBase_ID));
                foreach (var result in results)
                {
                    vendorIDs.Add(result.ValueAddedNetworkTraceNumber_VC);
                }
                return vendorIDs;
            }
        }
    }
}
