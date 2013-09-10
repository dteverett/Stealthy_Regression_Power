using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorUploadService;

namespace VendorAPI
{
    class Helper
    {
        /// <summary>
        /// Used Only By Vendor Project, Needs Switch To Production Setting When Ready
        /// </summary>
        /// <param name="claimsToDelete"></param>
        internal static void CleanUpVendorBatch(string claimsToDelete)
        {
            //DatabaseCalls.CleanUpVendorBatch(claimsToDelete);      // - PRODUCTION SETTING
            TestDatabaseCalls.CleanUpVendorBatch(claimsToDelete);    // - DEVELOPMENT SETTING
        }
    }
}
