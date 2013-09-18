using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OneTouchUploadProduction;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestLibrary;

namespace WebsiteRegressionProduction
{
    /// <summary>
    /// SuperClass Test contains all variables and methods that are used by all classes that extend Test, including and especially
    /// SetupTestGeneric() and TearDownTestGeneric()
    /// </summary>
    public class Test
    {
        protected PackageFactory packageFactory = new PackageFactory();
        protected OneTouchUploadPackage package;
        protected Client client;
        protected string batch;

        protected StringBuilder verificationErrors;
        protected bool isDuplicate = false;
        protected bool isFailed = false;
        protected System.Reflection.MethodBase method;
        private bool reachedEndOfTest;
        protected string errors;
        protected bool isFailedTest = false;
        protected bool isUploaded = false;
        
        /// <summary>
        /// 
        /// </summary>
        public void SetupTestGeneric()
        {
            reachedEndOfTest = false;
            method = null;
            verificationErrors = new StringBuilder();           
        }

        /// <summary>
        /// 
        /// </summary>
        public void TearDownTestGeneric()
        {
            if (!String.IsNullOrEmpty(errors))
            {
                errors = verificationErrors.ToString();
                if (errors.Length > 0)
                {
                    Assert.Fail(errors);
                }
                if (!reachedEndOfTest)
                {
                    Logger.logResults(method, Results.Fail, "DID NOT REACH END-OF-TEST.  " + errors);
                }
            }
            else if(reachedEndOfTest == false)
            {
                Logger.logResults(method, Results.Incomplete, "Test Never Initialized; Method was terminated prior to test interfacing");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void endOfTest()
        {
            errors = verificationErrors.ToString();
            reachedEndOfTest = true;
            Logger.logResults(method, errors.Length > 0 ? Results.Fail : Results.Pass, errors);
        }

    }
}
