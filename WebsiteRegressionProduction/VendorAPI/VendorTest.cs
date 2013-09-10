using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestLibrary;
using VendorUploadService;

namespace VendorAPI
{
    class VendorTest
    {
        protected Client client;
        protected VendorPackage package;
        protected StringBuilder verificationErrors;
        protected bool isFailed = false;
        protected System.Reflection.MethodBase method;
        protected bool reachedEndOfTest;
        protected string errors;
        protected bool isFailedTest = false;


        public void SetupTestGeneric()
        {
            reachedEndOfTest = false;
            method = null;
            verificationErrors = new StringBuilder();
        }

        public void TearDownTestGeneric()
        {
            errors = verificationErrors.ToString();
            if (errors.Length > 0)
            {
                Assert.Fail(errors);
            }
            if (!reachedEndOfTest)
            {
                Logger.logResults(method, TestLibrary.Results.Fail, "DID NOT REACH END-OF-TEST.  " + errors);
            }
        }


        public void endOfTest()
        {
            errors = verificationErrors.ToString();
            reachedEndOfTest = true;
            Logger.logResults(method, errors.Length > 0 ? TestLibrary.Results.Fail : TestLibrary.Results.Pass, errors);
        }
    }
}
