using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TestLibrary;
using VendorUploadService;
using VendorUploadService.ServiceReference1;


namespace VendorAPI
{
    [TestFixture]
    class VendorAPI_Validations : VendorTest
    {

        #region Vendor Claim Arrays
        private static int numUpperBoundClaims = 20;
        private static string[] upperBoundClaims = new string[] { "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO", "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "MESS025521419GOLDENRULE"
                                                                    , "MARS017642950AETNA98110", "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO", "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222"     
                                                                    };

        //private static int numMaxVendorClaims = 60;
        private static string[] maxVendorClaimIDs = new string[] { "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "MESS025521419GOLDENRULE", "MARS017642950AETNA98110", "JONE042984108BC", "A318628165202BC", "A153730769892DESERETMUT", "A105771313704MEDICARECO",
                                                                    "A101393808876AARP000002", "WOOD057491995ALTIUS", "WOOD055953054ALTIUS", "WAKE030025420UHC30555", "STIL021648542CIGNA18222", "1008884759330MCRB", "1008867585231MCRB", "1008888571486MCRB", "1008843631248MCRB","1015377224124118"
                                                                     ,"1025866576715SELF","1025872994407SELF","1025818712995MEDI000005","1025817238799MEDI000005","1025893443279MEDI000005","1026486898665CIGNAIND","1026414118819CIGNAIND","1026439834282CIGNAIND","1026424989355CIGNAIND","1026434591477CIGNAIND"
                                                                     ,"1026437990752CIGNAIND","1026438204869CIGNAIND","1026411736224CIGNAIND","1026448557203CIGNAIND","1026437784879CIGNAIND","1026431045953CIGNAIND","1026566329408DIM","1026576565321BC","102651400704BC","1033433822534CIGNAIND"   
                                                                      ,"1033461985389CIGNAIND","103598221367611","1036351152698MCR","1037213473451BLUE000010","1037231393900MCRB","1037268521958MCRB","1037296503219MCRB","1037996673945MCR","1037966647527MCR","1037926104464MCR"  
                                                                        ,"1037916280578MCR","1037981349751MCR","1037951295840MCR","1037917313539MCR","1039420268972MM","1039881534162MCR","104275606683155555","104277359676055555","104272974437455555","104279879747555555" 
                                                                        };
        private static int numUnfailedVendorClaimIDs = 46;
        private static string[] unfailedVendorClaimIDs = new string[] { "12345", "12100012120100250074", "12100009900100250151", "12100012120100251493", "12100007990100251773", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", 
                                                                        "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", 
                                                                        };

        private static int numRealAndImaginedClaims = 7;
        private static string[] realAndimaginedClaims = new string[] {"WAKE030025420UHC30555","thisisAMAdeUpClAiM", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "4449494949499944949949494", "011001110100011010110", "A101393808876AARP000002"
                                                                     };

        //private static int numUnfailedVendorClaimIDs = 200;
        //private static string[] unfailedVendorClaimIDs = new string[] { "12345", "12100012120100250074", "12100009900100250151", "12100012120100251493", "12100007990100251773", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", 
        //                                                                "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", 
        //                                                                "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", 
        //                                                                "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336", "12345", "12100012120100250074", "12100009900100250151", "12100012120100251493", "12100007990100251773", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336","0002875068723BCBSFED","0002891680158BCBSFED","0002837459754BCBSFED","000288509873BCBSFED","0002842221375BCBSFED","0002866584402BCBSFED","000289932005BCBSFED","0002871856747BCBSFED","0002820036202BCBSFED","0002841822535BCBSFED","0002820751250BCBSFED","0002818138855BCBSFED","0004336391821HORIZ00002","000437590978HORIZ00002","0004357945803HORIZ00002","0004327707715HORIZ00002","0004382693105HORIZ00002","0004394578295HORIZ00002","0004494190962HORIZ00002","0004495165413HORIZ00002" };
        //private static string[] unfailedVendorClaimIDs = new string[] { "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "WOOD057491995ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "A153730769892DESERETMUT", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "MESS025521419GOLDENRULE", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A318628165202BC", "JONE042984108BC", "MARS017642950AETNA98110", "STIL021648542CIGNA18222", "WAKE030025420UHC30555", "WOOD055953054ALTIUS", "A101393808876AARP000002", "A105771313704MEDICARECO", "0002875068723BCBSFED", "000288509873BCBSFED", "0002842221375BCBSFED", "0002866584402BCBSFED", "0002871856747BCBSFED", "0002820036202BCBSFED", "0002841822535BCBSFED", "0002820751250BCBSFED", "0002818138855BCBSFED", "000437590978HORIZ00002", "0004336391821HORIZ00002", "0004357945803HORIZ00002", "0004327707715HORIZ00002", "0004382693105HORIZ00002", "0004394578295HORIZ00002", "0004494190962HORIZ00002", "0004495165413HORIZ00002", "0004449480934HORIZ00002", "0004467713360HORIZ00002", "0004414341976HORIZ00002", "138336", "138336" };
        
        #endregion

        private const string MultiClaimValidationsBatch = @"VendorFiles\ValidationFiles\ValidationsBatch01.RMO";
        private const string getValidationsBatchNum = "0205131052NOA";
        private const string paperBatch = "0903131509QQM";
        private string[] EditsClaims = new string[] { "QATag.001", "QATag.002" };

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
        public void TheVendorAPI_VendorValidations_MultipleClaimIDsTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            bool[] errors = new bool[10];
            package = VendorPackage.createPackage(client,
                Document.CreateDocument(DocumentType.MedicalClaim, MultiClaimValidationsBatch));
            var validationResults = UploadService.CallUploadService(package);

            for (int j = 0; j < validationResults.claimResults.Length; ++j)
            {
                var currentResult = validationResults.claimResults[j];
                for (int i = 0; i < errors.Length; ++i)
                {
                    errors[i] = false;
                }
                if (j == 0)
                {
                    try
                    {
                        Assert.AreEqual(1, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected 1 validation to be returned with claim but recieved " +
                                                  currentResult.Errors.Length.ToString(CultureInfo.InvariantCulture) +
                                                  " errors");
                    }
                    try
                    {
                        Assert.AreEqual("Insured/Subscriber ID is missing.", currentResult.Errors[0].ErrorMessage); //Validation_T 3
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Unexpected Validation Error Returned For Claim 0: Brown, Devin");
                    }
                }
                if (j == 1)
                {
                    try
                    {
                        Assert.AreEqual(5, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Five Errors to be Returned with Claim 1 But Recieved " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 5)
                        {
                            verificationErrors.Append(
                                "Claim 1 returned with less than 5 errors which caused this test to Assert a Fail");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;

                        if (error == "On line item 1, the Procedure Code is missing.") //Validation_T 20
                            errors[i] = true;
                        if (error ==
                            "On line item 2, the Procedure Code is invalid. Procedure codes must be 5 digits in length.  Please correct the Procedure Code and resubmit the claim.") //Validation_T 24
                            errors[i] = true;
                        if (error ==
                            "On line item 3, the Unit of Measurement basis code must be selected.") //Validation_T 190
                            errors[i] = true;
                        if (error ==
                            "On line item 3, the NDC Unit or Basis of Measurement is not present.")  //Validation_T 275
                            errors[i] = true;
                        if (error == "On line item 3, the NDC Quantity is not present.") //Validation_T 276
                            errors[i] = true;
                    }

                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append("Unable to Locate Correct Error Message For Validation Check # " +
                                                      i +
                                                      " For Element Claim " + j);
                        }
                    }
                }
                if (j == 2)
                {
                    try
                    {
                        Assert.AreEqual(6, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append(
                            "Expected 6 Verification Errors returned by Claim 2: Eyre, Patricia, but " +
                            currentResult.Errors.Length + " Errors were returned");
                        if (currentResult.Errors.Length < 6)
                        {
                            verificationErrors.Append(
                                "Claim 2 Uploading with Less Than 6 Claims Causes This Test to Assert a Fail");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error == "Diagnosis Code 1 is invalid.") //Validation_T 114
                            errors[i] = true;
                        if (error ==
                            "Diagnosis Code 1 is not valid for the given Date of Service.") //Validation_T 115
                            errors[i] = true;
                        if (error == "Diagnosis Code 2 is invalid.") //Validation_T 117
                            errors[i] = true;
                        if (error ==
                            "Diagnosis Code 2 is not valid for the given Date of Service.") //Validation_T 118
                            errors[i] = true;
                        if (error ==
                            "On line item 1, the Procedure Code is not valid for the given Date of Service.")
                            //Validation_T 34
                            errors[i] = true;
                        if (error ==
                            "On line item 1, the Date of Service is missing or invalid.") //Validation_T 35
                            errors[i] = true;
                        //if (error == "On line item 1, Diagnosis Pointer 1 is invalid.")
                        //    //Validation_T 243
                        //    errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append("Unable to Find Correct Validation Error # " + i +
                                                      " For Element Claim " + j);
                        }
                    }
                }
                if (j == 3)
                {
                    try
                    {
                        Assert.AreEqual(1, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Claim # " + j +
                                                  " To be Returned with 1 Error, but claim returned with " +
                                                  currentResult.Errors.Length + " errors");
                        if (currentResult.Errors.Length < 1)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 1 error is returned");
                            Assert.Fail();
                        }
                    }
                    errors[0] =
                        currentResult.Errors[0].ErrorMessage.Contains("Procedure codes must be 5 digits in length"); //Edits_T 468
                    if (!errors[0])
                    {
                        verificationErrors.Append("Unable to Find Correct Validation Error # 1" +
                                                      " For Element Claim " + j);
                    }
                }
                if (j == 4)
                {
                    try
                    {
                        Assert.AreEqual(6, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected 6 Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 6)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 6 errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error ==
                            "Insured/Subscriber Date of Birth is missing or invalid.") //Validation_T 42
                            errors[i] = true;
                        if (error == "Diagnosis Code 1 is invalid.") //Validation_T 114
                            errors[i] = true;
                        if (error == "Diagnosis Code 1 is not valid for the given Date of Service.") //Validation_T 115
                            errors[i] = true;
                        if (error == "E-Code can not be the primary Diagnosis Code.") //Validation_T 254
                            errors[i] = true;
                        if (error == "Initial Treatment Date must be later than Patient Date of Birth.") //Validation_T 328
                            errors[i] = true;
                        if (error == "Last Seen Date must be later than Patient Date of Birth.") //Validation_T 333
                            errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 5)
                {
                    try
                    {
                        Assert.AreEqual(4, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected 3 Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 3)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 3 errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error ==
                            "The payer address you are using has been marked as invalid in the Apex system. Please verify the address with the payer and correct the address.")
                            errors[i] = true;
                        if (error ==
                            "On line item 2, the Unit of Measurement basis code must be selected.") //Validation_T 190
                            errors[i] = true;
                        if (error ==
                            "On line item 2, the NDC Unit or Basis of Measurement is not present.") //Validation_T 275
                            errors[i] = true;
                        if (error ==
                            "On line item 2, the NDC Quantity is not present.") //Validation_T 276
                            errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 6)
                {
                    try
                    {
                        Assert.AreEqual(1, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Claim # " + j +
                                                  " To be Returned with 1 Error, but claim returned with " +
                                                  currentResult.Errors.Length + " errors");
                        if (currentResult.Errors.Length < 1)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 1 error is returned");
                            Assert.Fail();
                        }
                    }
                    errors[0] =
                        currentResult.Errors[0].ErrorMessage.Contains("Patient First Name is missing."); //Validation_T 6
                    if (!errors[0])
                    {
                        verificationErrors.Append("Unable to Find Correct Validation Error # 1" +
                                                      " For Element Claim " + j);
                    }
                }
                if (j == 7)
                {
                    try
                    {
                        Assert.AreEqual(7, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected 7 Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 7)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 7 errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error == "Referring Provider NPI is missing.") //Validation_T 69
                            errors[i] = true;
                        if (error == "Diagnosis Code 1 is missing.") //Validaition_T 113
                            errors[i] = true;
                        if (error == "Diagnosis Code 2 is missing.") //Validation_T 116
                            errors[i] = true;
                        if (error == "On line item 1, Diagnosis Pointer 1 is invalid.") //Validation_T 243
                            errors[i] = true;
                        if (error == "On line item 1, Diagnosis Pointer 2 is invalid.") //Validation_T 244
                            errors[i] = true;
                        if (error == "On line item 2, Diagnosis Pointer 1 is invalid.") //Validation_T 243
                            errors[i] = true;
                        if (error == "On line item 2, Diagnosis Pointer 2 is invalid.") //Validation_T 244
                            errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 8)
                {
                    try
                    {
                        Assert.AreEqual(3, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected 3 Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 3)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 3 errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error == "Insured/Subscriber Address is missing or invalid.") //Validation_T 38
                            errors[i] = true;
                        if (error == "The Subscriber must contain a valid address under Subscriber or Patient when relationship is self.") //Validation_T 405
                            errors[i] = true;
                        if (error == "Patient Address is missing or invalid.") //Validation_T 51
                            errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 9)
                {
                    int expectedNumOfErrors = 6;
                    try
                    {
                        Assert.AreEqual(expectedNumOfErrors, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {

                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected " + expectedNumOfErrors + " Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < expectedNumOfErrors)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than " + expectedNumOfErrors + " errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error == "The payer address you are using has been marked as invalid in the Apex system. Please verify the address with the payer and correct the address.") //Validation_T 1
                            errors[i] = true;
                        if (error == "Payer Name is missing.") //Validation_T 44
                            errors[i] = true;
                        if (error == "Payer Address Line 1 is missing or invalid.") //Validation_T 45
                            errors[i] = true;
                        if (error == "Payer City is missing or invalid.") //Validation_T 46
                            errors[i] = true;
                        if (error == "Payer State is missing or invalid.") //Validation_T 47
                            errors[i] = true;
                        if (error == "Payer Zip Code is missing or invalid.") //Validation_T 48
                            errors[i] = true;
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 10)
                {
                    try
                    {
                        Assert.AreEqual(0, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Claim Test # " + j +
                                                  " To Be Returned with Zero Errors, But Claim Returned with " +
                                                  currentResult.Errors.Length + " Errors");
                    }
                }
                if (j == 11)
                {
                    try
                    {
                        Assert.AreEqual(1, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Claim # " + j +
                                                  " To be Returned with 1 Error, but claim returned with " +
                                                  currentResult.Errors.Length + " errors");
                        if (currentResult.Errors.Length < 1)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 1 error is returned");
                            Assert.Fail();
                        }
                    }
                    errors[0] =
                        currentResult.Errors[0].ErrorMessage.Contains("Auto Accident State Code is missing or invalid."); //Validation_T 277
                    if (!errors[0])
                    {
                        verificationErrors.Append("Unable to Find Correct Validation Error # 1" +
                                                      " For Element Claim " + j);
                    }
                }
                if (j == 12)
                {
                    try
                    {
                        Assert.AreEqual(8, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Claim Test # " + j +
                                                  " Expected 8 Validation Errors, but Claim Returned " +
                                                  currentResult.Errors.Length + " Errors");
                        if (currentResult.Errors.Length < 8)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 8 errors are returned");
                            Assert.Fail();
                        }
                    }
                    for (int i = 0; i < currentResult.Errors.Length; i++)
                    {
                        string error = currentResult.Errors[i].ErrorMessage;
                        if (error == "Diagnosis Code 3 is missing.") //Validation_T 119
                            errors[i] = true;
                        if (error == "Diagnosis Code 4 is missing.") //Validation_T 122
                            errors[i] = true;
                        if (error == "Diagnosis Code 5 is missing.") //Validation_T 125
                            errors[i] = true;
                        if (error == "Diagnosis Code 6 is missing.") //Validation_T 128
                            errors[i] = true;
                        if (error == "On line item 3, Diagnosis Pointer 1 is invalid.") //Validation_T 243
                            errors[i] = true;
                        if (error == "On line item 3, Diagnosis Pointer 2 is invalid.") //Validation_T 244
                            errors[i] = true;
                        if (error == "On line item 3, Diagnosis Pointer 3 is invalid.") //Validation_T 245
                            errors[i] = true;
                        if (error == "On line item 3, Diagnosis Pointer 4 is invalid.") //Validation_T 246
                            errors[i] = true;
                        //Better way to accomplish same thing 
                        //if(currentResult.Errors.Any(x => x == ""))
                    }
                    for (int i = 0; i < currentResult.Errors.Length; ++i)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[i]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append(
                                "Unable To Find Expected Validation Error # " + i + " Returned for Claim Test # " + j);
                        }
                    }
                }
                if (j == 13)
                {
                    try
                    {
                        Assert.AreEqual(1, currentResult.Errors.Length);
                    }
                    catch (Exception)
                    {
                        verificationErrors.Append("Expected Claim # " + j +
                                                  " To be Returned with 1 Error, but claim returned with " +
                                                  currentResult.Errors.Length + " errors");
                        if (currentResult.Errors.Length < 1)
                        {
                            verificationErrors.Append("Test " + j + " Assers a Fail if less than 1 error is returned");
                            Assert.Fail();
                        }
                    }
                    errors[0] =
                        currentResult.Errors[0].ErrorMessage.Contains(
                            " Date of Service is after the 'FROM' Date of Service. Please correct and resubmit the claim."); //Validation_T 23
                    if (!errors[0])
                    {
                        verificationErrors.Append("Unable to Find Correct Validation Error # 1" +
                                                      " For Element Claim " + j);
                    }
                }
            }
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_VendorValidations_SingleClaimIDTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            bool[] errors = new bool[5];
            package = VendorPackage.createPackage(client,
                Document.CreateDocument(DocumentType.MedicalClaim,
                    @"VendorFiles\ValidationFiles\SingleClaimIDTestBatch.BRF"));
            var results = UploadService.CallUploadService(package);
            //var claimID = results.claimResults[0].VendorClaimId;
            var validations = results.claimResults[0].Errors;
            try
            {
                Assert.AreEqual(2, validations.Length);
            }
            catch (Exception)
            {
                verificationErrors.Append("Expected Claim Test to Return 2 Validation Errors but Instead Received " +
                                          validations.Length + " Error(s)");
                if (validations.Length < 2)
                {
                    verificationErrors.Append("Test Asserts a Fail if Upload Returns Less Than 2 Errors");
                    Assert.Fail();
                }
            }
            for (int i = 0; i < validations.Length; ++i)
            {
                var error = validations[i].ErrorMessage;
                if (error ==
                    "Claim was received with an unknown provider. Please select a provider on the form or contact your Apex Account Manager. ") //Validation_T 10
                    errors[i] = true;
                if (error == "Referring Provider Last Name is missing.") //Validation_T 67
                    errors[i] = true;
            }
            for (int i = 0; i < validations.Length; ++i)
            {
                try
                {
                    Assert.AreEqual(true, errors[i]);
                }
                catch (Exception)
                {
                    verificationErrors.Append(
                        "Claim Verifications Did Not Return Expected Validation Error Messages For Validation Test #" +
                        i);
                }
            }
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_CallGetValidationMessagesPerClaimTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            bool[] errors = new bool[10];
            client = new Client("NOA", "novarad", "apex10");
            var results = UploadService.GetMultiValidationErrors(client, getValidationsBatchNum);  //batch num 0205131052NOA
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; ++i)
            {
                for (int j = 0; j < errors.Length; j++)
                {
                    errors[j] = false;
                }
                if (i == 0)
                {
                    for (int j = 0; j < results.MultiClaimValidationResponses[i].Errors.Length; ++j)
                    {
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "The payer address you are using has been marked as invalid in the Apex system. Please verify the address with the payer and correct the address.")
                            errors[0] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Insured/Subscriber ID is missing.")
                            errors[1] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "If the Primary Paid Amount and Adjudication Date are supplied, the claim must specify Other Subscriber Last Name.")
                            errors[2] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Diagnosis Code 1 is invalid.")
                            errors[3] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Diagnosis Code 1 is not valid for the given Date of Service.")
                            errors[4] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "On line item 1, the Procedure Code is invalid. Procedure codes must be 5 digits in length.  Please correct the Procedure Code and resubmit the claim.")
                            errors[5] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage ==
                            "The Primary Paid Amount, Patient Responsible Amount, and Adjustment Amount do not sum up to the charged amount on line item 1.  Please double check and correct the amounts entered and resubmit the claim.")
                            errors[6] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage ==
                            "All line item Payer Paid Amounts must sum up to the Claim level Payer Paid Amount")
                            errors[7] = true;
                    }
                    for (int j = 0; j < results.MultiClaimValidationResponses[i].Errors.Length; j++)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[j]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append("Claim " + i +
                                                      " Did Not Return Expected Validation Error Message #" + j);
                        }
                    }
                }
                if (i == 1)
                {
                    for (int j = 0; j < results.MultiClaimValidationResponses[i].Errors.Length; ++j)
                    {
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Insured/Subscriber ID is missing.")
                            errors[0] = true;
                        //if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Referring Provider Secondary ID Type must be selected.")
                         //   errors[1] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Diagnosis Code 3 is invalid.")
                            errors[1] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "Diagnosis Code 3 is not valid for the given Date of Service.")
                            errors[2] = true;
                        if (results.MultiClaimValidationResponses[i].Errors[j].ErrorMessage == "On line item 1, the Procedure Code is invalid. Procedure codes must be 5 digits in length.  Please correct the Procedure Code and resubmit the claim.")
                            errors[3] = true;
                    }
                    for (int j = 0; j < results.MultiClaimValidationResponses[i].Errors.Length; j++)
                    {
                        try
                        {
                            Assert.AreEqual(true, errors[j]);
                        }
                        catch (Exception)
                        {
                            verificationErrors.Append("Claim " + i +
                                                      " Did Not Return Expected Validation Error Message #" + j);
                        }
                    }
                }
            }
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_CallValidationsOnClaimsBelongingToDifferentClientTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            var results = UploadService.GetMultiValidationErrors(client, getValidationsBatchNum);
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; i++)
            {
                try
                {
                    Assert.AreEqual(false, results.MultiClaimValidationResponses[i].ClaimWasFound);
                }
                catch (Exception)
                {
                    verificationErrors.Append(
                        "Claims Returned With ClaimWasFound boolean of True, Expected ClaimWasFound = False");
                }
            }
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_CallValidationsOnClaimThatHasIdenticalVendorIdentifierBelongingToDifferentClientTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            client = new Client("CYE", "cyclops", "apex10");
            var results = UploadService.GetMultiValidationErrors(client, getValidationsBatchNum);
            int numOfResults = 0;
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; ++i)
            {
                if (results.MultiClaimValidationResponses[i] != null)
                {
                    ++numOfResults;
                }
            }
            try
            {
                Assert.AreEqual(2, numOfResults);
            }
            catch (Exception)
            {
                verificationErrors.Append(
                    "Test Expected 2 Claim Results Returned But More than 2 results Returned.  It Appears Claims From Multiple Clients Are Being Returned");
                Assert.Fail();
            }
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_LoadBearingTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            //LoadRunner.runLoadBearingVendor();
            LoadRunner.testRunLoadBearingVendor(Credentials.MedicalClient5010);

            endOfTest();
        }

        [Test]
        public void TheVendorAPI_GetMultipleValidationsUpperBoundTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            int length = numUpperBoundClaims; // - 20, as per Requirement Wiki
            var results = UploadService.GetMultiValidationErrors(Credentials.MedicalClient5010, upperBoundClaims);
            try
            {
                Assert.AreEqual(numUpperBoundClaims, results.MultiClaimValidationResponses.Length);
            }
            catch (Exception)
            {
                verificationErrors.Append("Uploaded Array Of " + numUpperBoundClaims +
                                          " Claims But the Incorrect Number of Responses Was Returned. Expected: " +
                                          numUpperBoundClaims + " Actual: " + results.MultiClaimValidationResponses.Length);
            }
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; i++)
            {
                try
                {
                    Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                }
                catch (Exception)
                {
                    verificationErrors.Append("Did Not Receive Expected Apex Validation Response For Claim #" + i);
                }
            }
            endOfTest();
        }
        /// <summary>
        /// Test is no longer relevent since max num of uploads is below the necessary level needed to make configuration changes
        /// Test has been commented out and does not run any tests on any method or procedure
        /// </summary>
        [Test]
        public void TheVendorAPI_GetMultipleValidationsWithoutExtraConfigurationsUpperBoundTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            //int length = smallVendorClaimIDs.Length;
            //var results = UploadService.GetMultipleValidationResultsWithoutChanges(Credentials.MedicalClient5010, smallVendorClaimIDs);
            //try
            //{
            //    Assert.AreEqual(numSmallVendorClaimIDs, results.MultiClaimValidationResponses.Length);
            //}
            //catch (Exception)
            //{
            //    verificationErrors.Append("Uploaded Array Of " + numSmallVendorClaimIDs +
            //                              " Claims But the Incorrect Number of Responses Was Returned. Expected: " +
            //                              numSmallVendorClaimIDs + " Actual: " + results.MultiClaimValidationResponses.Length);
            //}
            //for (int i = 0; i < results.MultiClaimValidationResponses.Length; i++)
            //{
            //    try
            //    {
            //        Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
            //    }
            //    catch (Exception)
            //    {
            //        verificationErrors.Append("Did Not Receive Expected Apex Validation Response For Claim #" + i);
            //    }
            //}
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_GetMultipleValidationsWithZeroErrorClaimsUpperBoundTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            var results = UploadService.GetMultiValidationErrors(Credentials.MedicalClient5010, unfailedVendorClaimIDs);
            try
            {
                Assert.AreEqual(numUnfailedVendorClaimIDs, results.MultiClaimValidationResponses.Length);
            }
            catch (Exception)
            {
                verificationErrors.Append("Uploaded Array Of " + numUnfailedVendorClaimIDs +
                                          " Claims But the Incorrect Number of Responses Was Returned. Expected: " +
                                          numUnfailedVendorClaimIDs + " Actual: " + results.MultiClaimValidationResponses.Length);
            }
            for (int i = 0; i < results.MultiClaimValidationResponses.Length; i++)
            {
                try
                {
                    Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                }
                catch (Exception)
                {
                    verificationErrors.Append("Did Not Receive Expected Apex Validation Response For Claim #" + i);
                }
            }

            endOfTest();
        }

        [Test]
        public void TheVendorAPI_CallGetValidationsOnDeletedClaimTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

        }

        [Test]
        public void TheVendorAPI_CallValidationsOnNonExistentClaimTest()
        {
            
        }

        [Test]
        public void TheVendorAPI_CallMultiValidationOnRealAndImaginedClaimsTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            var results = UploadService.GetMultiValidationErrors(client, realAndimaginedClaims);
            try
            {
                Assert.AreEqual(numRealAndImaginedClaims, results.MultiClaimValidationResponses.Length);
            }
            catch (AssertionException)
            {
                verificationErrors.Append("Unexpected Number of Results Returned From Validations Call. Expected: " + numRealAndImaginedClaims + " But Was: " + results.MultiClaimValidationResponses.Length);
                if (results.MultiClaimValidationResponses.Length < numRealAndImaginedClaims)
                    Assert.Fail(); //Since each expected individual response is tested, the code will throw an outOfBounds exception if less than expected is returned
            }
            for (int i = 0; i < numRealAndImaginedClaims; ++i)
            {
                if (i == 0)
                {
                    try
                    {
                        Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                    try
                    {
                        Assert.AreEqual(9, results.MultiClaimValidationResponses[i].Errors.Length);
                    }
                    catch (AssertionException)
                    {
                        verificationErrors.Append("Unexpected Number Of Validation Errors Returned for Claim " + i);
                    }
                }
                if (i == 1)
                {
                    try
                    {
                        Assert.AreEqual(false, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
                if (i == 2)
                {
                    try
                    {
                        Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                    try
                    {
                        Assert.AreEqual(3, results.MultiClaimValidationResponses[i].Errors.Length);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
                if (i == 3)
                {
                    try
                    {
                        Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                    try
                    {
                        Assert.AreEqual(8, results.MultiClaimValidationResponses[i].Errors.Length);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
                if (i == 4)
                {
                    try
                    {
                        Assert.AreEqual(false, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
                if (i == 5)
                {
                    try
                    {
                        Assert.AreEqual(false, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
                if (i == 6)
                {
                    try
                    {
                        Assert.AreEqual(true, results.MultiClaimValidationResponses[i].ClaimWasFound);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                    try
                    {
                        Assert.AreEqual(2, results.MultiClaimValidationResponses[i].Errors.Length);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                    }
                }
            }
            
            endOfTest();
        }

        [Test]
        public void TheVendorAPI_CallValidationsOn4010PayerClaims()
        {
            method = new StackTrace().GetFrame(0).GetMethod();


        }

        [Test]
        public void TheVendorAPI_CalLValidationsOnPaperPayerClaims()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            var validationErrors = new bool[4];
            var results = UploadService.GetMultiValidationErrors(Credentials.StatementClient, EditsClaims);
            for (int j = 0; j < results.MultiClaimValidationResponses.Length; ++j)
            {
                for (int i = 0; i < errors.Length; ++i)
                {
                    validationErrors[j] = false;
                }
                if (j == 0)
                {
                    for (int i = 0; i < results.MultiClaimValidationResponses[j].Errors.Length; ++i)
                    {
                        if (results.MultiClaimValidationResponses[j].Errors[i].Equals("The submitted insured id is all the same digit. Apex has found this sort of insured id to be invalid. Please correct and resubmit."))
                            validationErrors[i] = true;
                    }
                }
                if (j == 1)
                {
                    for (int i = 0; i < results.MultiClaimValidationResponses[j].Errors.Length; ++i)
                    {
                        if (results.MultiClaimValidationResponses[j].Errors[i].Equals("The patient's first name is missing from this claim. Please resubmit the claim with the patient's first name."))
                            validationErrors[i] = true;
                    }
                }
                for (int i = 0; i < results.MultiClaimValidationResponses[j].Errors.Length; ++i)
                {
                    try
                    {
                        Assert.AreEqual(true, validationErrors[i]);
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append("Unable to find Error " + i + " for Claim " + j + "\n" + e.Message);
                    }
                }
            }
            endOfTest();
        }
    }
}
