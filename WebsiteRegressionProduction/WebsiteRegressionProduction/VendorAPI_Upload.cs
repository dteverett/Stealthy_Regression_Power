using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TestLibrary;
using VendorUploadService;
using VendorUploadService.ServiceReference1;

namespace WebsiteRegressionProduction
{
    [TestFixture]
    class VendorAPI_Upload : VendorTest
    {
        private const string SingleClaimNoErrors = @"VendorFiles\UploadFiles\SingleClaimNoErrorsBatch.BRF";
        //private const string MultiClaimNoErrors = @"VendorFiles\MultiClaimNoErrorsBatch.BRF";
        private const string MisMatchedBatch = @"VendorFiles\ValidationFiles\MisMatchedCredsBatch.BRF";
        //private const string MultiClaimValidationsBatch = @"VendorFiles\ValidationFiles\ValidationsBatch01.RMO";
        private const string halfRefBatch = @"VendorFiles\UploadFiles\halfRefBatch.HLD";
        //private const string fiftyClaimBatch = @"VendorFiles\UploadFiles\fiftyClaimBatch.RMO";
        //private const string twentyFiveClaimBatch = @"VendorFiles\UploadFiles\twentyfiveClaimBatch.RMO";
        //private const string onefiftyClaimBatch = @"VendorFiles\UploadFiles\oneHundredFiftyFourBatch.BAG";
        //private const string twoHundredClaimBatch = @"VendorFiles\UploadFiles\twoHundredClaimBatch.MTV";
        //private const string superMassiveBatch = @"VendorFiles\UploadFiles\superMassiveBatch.MYP";
        //private const string duplicateRefsBatch = @"VendorFiles\UploadFiles\duplicateRefD9s.RMO";
        private const string duplicateRefsBatch = @"VendorFiles\UploadFiles\duplicateRefD9s.RMO";
        private const string oneOverUpperBatch = @"VendorFiles\UploadFiles\TwentyOneClaimBatch.DIU";
        private const string upperBoundBatch = @"VendorFiles\UploadFiles\TwentyClaimBatch.BPB";
        private const string invalidBatch = @"VendorFiles\UploadFiles\invalidBatch.txt";
        private const string hundredTwoClaimsBatch = @"VendorFiles\UploadFiles\HundredPlusBatch.RIY";

        public static int FilesUploadedCounter;


        #region Vendor Claim Arrays
        //private static int numSmallVendorClaimIDs = 30;
        private static string[] smallVendorClaimIDs = new string[] { "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO", "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "MESS025521419GOLDENRULE"
                                                                    , "MARS017642950AETNA98110", "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO", "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222" 
                                                                    ,"104273976413155555","104271873302155555","1042985027505AETNA","1045866385017MCR","1045838883525MCR","1045896849759MCR","1045823644135MCR","1050465405644AENT000002","1053753029533MCR","1053753029533UNITEDAMER"        
                                                                    };

        //private static int numMaxVendorClaims = 60;
        private static string[] maxVendorClaimIDs = new string[] { "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "MESS025521419GOLDENRULE", "MARS017642950AETNA98110", "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO",
                                                                    "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "1008884759330MCRB", "1008867585231MCRB", "1008888571486MCRB", "1008843631248MCRB","1015377224124118"
                                                                     ,"1025866576715SELF","1025872994407SELF","1025818712995MEDI000005","1025817238799MEDI000005","1025893443279MEDI000005","1026486898665CIGNAIND","1026414118819CIGNAIND","1026439834282CIGNAIND","1026424989355CIGNAIND","1026434591477CIGNAIND"
                                                                     ,"1026437990752CIGNAIND","1026438204869CIGNAIND","1026411736224CIGNAIND","1026448557203CIGNAIND","1026437784879CIGNAIND","1026431045953CIGNAIND","1026566329408DIM","1026576565321BC","102651400704BC","1033433822534CIGNAIND"   
                                                                      ,"1033461985389CIGNAIND","103598221367611","1036351152698MCR","1037213473451BLUE000010","1037231393900MCRB","1037268521958MCRB","1037296503219MCRB","1037996673945MCR","1037966647527MCR","1037926104464MCR"  
                                                                        ,"1037916280578MCR","1037981349751MCR","1037951295840MCR","1037917313539MCR","1039420268972MM","1039881534162MCR","104275606683155555","104277359676055555","104272974437455555","104279879747555555" 
                                                                        };

        // static int numUnfailedVendorClaimIDs = 200;
        private static string[] unfailedVendorClaimIDs = new string[] { "12345", "12100012120100250074", "12100009900100250151", "12100012120100251493", "12100007990100251773", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336", "12345", "12100012120100250074", "12100009900100250151", "12100012120100251493", "12100007990100251773", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336","0002875068723BCBSFED","0002891680158BCBSFED","0002837459754BCBSFED","000288509873BCBSFED","0002842221375BCBSFED","0002866584402BCBSFED","000289932005BCBSFED","0002871856747BCBSFED","0002820036202BCBSFED","0002841822535BCBSFED","0002820751250BCBSFED","0002818138855BCBSFED","0004336391821HORIZ00002","000437590978HORIZ00002","0004357945803HORIZ00002","0004327707715HORIZ00002","0004382693105HORIZ00002","0004394578295HORIZ00002","0004494190962HORIZ00002","0004495165413HORIZ00002" };
        //private static string[] unfailedVendorClaimIDs = new string[] { "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336" };
        
        #endregion

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.MedicalClient5010;
            SetupTestGeneric();
        }

        [TearDown]
        public void TeardownTest()
        {
            TearDownTestGeneric();
        }

        [Test]
        public void TheVendorAPI_UploadMultipleVendorBatchesTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            string[] files = Directory.GetFiles("VendorFiles");
            var claimsToDelete = new string[1];

            foreach (var file in files)
            {
                Document document = Document.CreateDocument(DocumentType.MedicalClaim, file);
                package = VendorPackage.createPackage(client, document);
                var results = UploadService.CallUploadService(package);
                if (results.claimResults.Length == 0)
                {
                    verificationErrors.Append("Vendor API Service Returned an Unexpected Result of Null or Empty");
                    Assert.Fail("Vendor API Service Returned an Unexpected Result of Null or Empty");
                }
                try
                {
                    Assert.AreEqual(true, results.noErrors);
                }
                catch (Exception)
                {
                    verificationErrors.Append(
                        "Expected VendorAPI Batch to Have No Errors Returned, But Error(s) were returned");
                }

                if (file.Contains("Multi"))
                {
                   
                    for (int i = 0; i < results.claimResults.Length; i++)
                    {
                        try
                        {
                            Assert.AreEqual(ClaimDeletionStatus.ClaimNotFound,
                                results.claimResults[i].ClaimDeletionStatus);
                        }
                        catch (Exception e)
                        {
                            verificationErrors.Append(e.Message);
                        }
                        if (i == 0)
                        {
                            claimsToDelete[i] = results.claimResults[i].VendorClaimId;
                        }
                    }
                }
            }
            Helper.CleanUpVendorBatch(claimsToDelete);
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_UploadVendorReImportedClaimsTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(DocumentType.MedicalClaim, SingleClaimNoErrors));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(1, results.claimResults[0].Errors.Length);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Expected 1 Errors Returned From Batch Upload, but Batch Upload Returned With " +
                    results.claimResults[0].Errors.Length.ToString(CultureInfo.InvariantCulture) + " Errors");
            }
            try
            {
                Assert.AreEqual(ClaimDeletionStatus.ClaimWasReimported, results.claimResults[0].ClaimDeletionStatus);
            }
            catch (Exception)
            {
                verificationErrors.Append("Unexpected ClaimDeletionStatus Value: Expected ClaimWasReimported but was " +
                                          results.claimResults[0].ClaimDeletionStatus.ToString());
            }

            endOfTest();
        }



        [Test]
        public void TheVendorAPI_UploadClaimsWithoutRefD9Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = VendorPackage.createPackage(client,
                Document.CreateDocument(DocumentType.MedicalClaim, @"VendorFiles\NonRefD9Batches\NonRefD9Batch.NIH"));
            var results = UploadService.CallUploadService(package);

            if (results.thrownException == false)
            {
                verificationErrors.Append(
                    "Uploaded Batch Excepted to Return Validation Errors, but No Validation Errors Were Returned");
                Assert.Fail("Uploaded batch excpeted to return validation errors, but no validation errors were returned");
            }
            var exception = results.Exceptions[0];
            //bool isMatch = Regex.IsMatch(exception.Message, "FaultException");
            bool isMatch2 = Regex.IsMatch(exception.Message, "A Claim was sent without a Vendor Claim ID, No Claims Imported");
            try
            {
                Assert.AreEqual(true, isMatch2);
            }
            catch (Exception e)
            {
                verificationErrors.Append(
                    "Expected a Returned Error Message that Contained Correct Reason for Failed Uploaded, but Correct Message was Not Returned\n" + e.Message);
            }

            endOfTest();
        }

        [Test]
        public void TheVendorAPI_UploadClaimsWithMisMatchedCredentials()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            Client misMatchedCreds = Client.createClient("ZZZ", "demo1", "dental2");
            package = new VendorPackage(misMatchedCreds, Document.CreateDocument(DocumentType.MedicalClaim, MisMatchedBatch));
            var results = UploadService.CallUploadService(package);
            if (results.thrownException != true)
            {
                verificationErrors.Append("MisMatched Credentials Expected Thrown Exception but No Exception was Thrown");
                Assert.Fail("MisMatched Credentials Expected Thrown Exception but No Exception was Thrown");
            }
            bool isMatch2 = Regex.IsMatch(results.Exceptions[0].Message, "Invalid username or password");
            try
            {
                Assert.AreEqual(true, isMatch2);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Expected Error Message Containing Credentials Information but Error Message Was Not As Expected");
            }
            endOfTest();
        }

        
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TheVendorAPI_CallUploadMultipleClaimsSomeWithoutRefD9Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            package = new VendorPackage(client, Document.CreateDocument(halfRefBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(true, results.thrownException);
            }
            catch (Exception)
            {
                verificationErrors.Append("Expected Exception To Be Thrown But No Exception Was Thrown");
            }
            bool isMatch = Regex.IsMatch(results.Exceptions[0].Message,
                "Missing Vendor Claim Id: A Claim was sent without a Vendor Claim ID, No Claims Imported");
            try
            {
                Assert.AreEqual(true, isMatch);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Unexpected Exception Thrown From Upload Service, Expected Error, \"A Claim was sent without a Vendor Claim ID\"");
            }


            endOfTest();
        }

        [Test]
        public void TheVendorAPI_UploadLargeBatchTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(hundredTwoClaimsBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(true, results.thrownException);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Expected 100 claim batch to throw exceptions, but no exception(s) were thrown");
            }
            endOfTest();
        }


        /// <summary>
        /// Tests over the upper-bound of number of claims that can be sent
        /// </summary>
        [Test]
        public void TheVendorAPI_UploadOneClaimOverLimitTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(oneOverUpperBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(true, results.thrownException);
            }
            catch (Exception)
            {
                verificationErrors.Append("Expected 21 claim batch to throw an exception, but no exception(s) were thrown");
            }
            try
            {
                Assert.AreEqual(/*TODO Insert Message*/"", results.Exceptions[0].Message);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            endOfTest();
        }

        /// <summary>
        /// Tests the upper-bound to verify that max num of claims allowed works as expected
        /// </summary>
        [Test]
        public void TheVendorAPI_UploadUpperBoundLimitBatchTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(upperBoundBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(false, results.thrownException);
            }
            catch (Exception)
            {
                verificationErrors.Append("Expected 20 claim batch to throw no exceptions, but exception(s) were thrown");
            }

            endOfTest();
        }

        [Test] //Bug 2849
        public void TheVendorAPI_UploadBatchWithDuplicateRefD9Segments()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(duplicateRefsBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual(true, results.thrownException);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Expected Batch That Uploaded With Duplicate Ref D9s To Throw Exception But No Exceptions Were Thrown");
            }
            try
            {
                Assert.AreEqual("Batch import failed.  Batch contains claims with duplicate REF*D9 segments.", results.Exceptions[0].Message);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            endOfTest();
        }

        /// <summary>
        /// Incomplete.  This test does not apply under the current requirements but has been logged in the defect baglog
        /// </summary>
        [Test]
        public void TheVendorAPI_UploadInvalidBatch()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            package = new VendorPackage(client, Document.CreateDocument(invalidBatch));
            var results = UploadService.CallUploadService(package);
            try
            {
                Assert.AreEqual("", results.Exceptions[0].Message);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            endOfTest();
        }
    }
}
    