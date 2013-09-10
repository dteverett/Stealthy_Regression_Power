using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TestLibrary;
using VendorUploadService;
using VendorUploadService.ServiceReference1;


namespace VendorAPI
{
    [TestFixture]
    class VendorAPI_Delete : VendorTest
    {
        private const string batchToDelete = @"VendorFiles\ValidationFiles\batchToDelete.AHT";
        private const string fakeClaimToDelete = "00557jjkhfutypee";
        private const string claimBelongingToDifferentClient = "JUAR065761825MEDI000001";


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
        /// <summary>
        /// Setup: Uploads a batch of claims using demo1 and stores the vendor ID from each claim
        /// Test: Foreach stored Vendor ID from setup, call delete service on that claim
        /// Expected Result: 23 results all with the ClaimDeletionStatus of Deleted
        /// </summary>
        [Test]
        public void TheVendorAPI_DeleteVendorClaimsTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();

            var claimsToDelete = new string[23];
            package = VendorPackage.createPackage(client,
                Document.CreateDocument(DocumentType.MedicalClaim, batchToDelete));
            var uploadResults = UploadService.CallUploadService(package);
            for (int i = 0; i < claimsToDelete.Length; i++)
            {
                claimsToDelete[i] = uploadResults.claimResults[i].VendorClaimId;
            }
            var deleteResults = UploadService.CallDeleteClaimService(package, claimsToDelete);
            foreach (var result in deleteResults.claimDeletionStatuses)
            {
                try
                {
                    Assert.AreEqual(ClaimDeletionStatus.Deleted, result);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
            endOfTest();
        }

        /// <summary>
        /// Call delete service on made up claim that will not match anything in the database
        /// Expected Result: Returned a ClaimDeletionStatus of ClaimNotFound
        /// </summary>
        [Test]
        public void TheVendorAPI_CallDeleteClaimOnNonExistentClaim()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            var results = UploadService.CallDeleteClaimService(client, fakeClaimToDelete);
            try
            {
                Assert.AreEqual(ClaimDeletionStatus.ClaimNotFound, results.claimDeletionStatus);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            endOfTest();
        }
        /// <summary>
        /// Test calls delete service credentials demo1 using vendor ID belonging to client MWT
        /// Expected result returned is ClaimDeletionStatus.ClaimNotFound
        /// </summary>
        [Test]
        public void TheVendorAPI_CallDeleteClaimOnClaimThatDoesNotBelongToClientTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            var results = UploadService.CallDeleteClaimService(client, claimBelongingToDifferentClient);
            try
            {
                Assert.AreEqual(ClaimDeletionStatus.ClaimNotFound, results.claimDeletionStatus);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            endOfTest();
        }
    }
}
