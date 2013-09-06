using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OneTouchUploadProduction;
using TestLibrary;

namespace WebsiteRegressionProduction
{
    [TestFixture]
    class Statements : Test
    {
        private const string FILEUNDERTEST = @"Files\Revolution7claim.NIO";
        private const DocumentType type = DocumentType.Statement;
        private IWebDriver driver;
        private bool acceptNextAlert;

        [SetUp]
        public void SetupTest()
        {
            client = Credentials.StatementClient;
            driver = new FirefoxDriver();
            package = packageFactory.createPackage(client, FILEUNDERTEST, type);
            
            SetupTestGeneric();
            if (!driver.Login(client))
            {
                //TODO: Add logging or console
            }
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
                Helper.DeleteBatch(batch, package);
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            TearDownTestGeneric(); 
        }

        [Test]
        public void TheStatements_Add_A_Batch_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("A1"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddBatchButton"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_ClaimTypeBatchRadioButtonList_1"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_StatementCreateButton"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"),10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"),10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"),10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_btnReleaseBatch")).Click();
            try
            {
                Assert.AreEqual("Processed", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }

        [Test]
        public void TheStatements_Add_A_Statement_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("A1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddBatchButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_ClaimTypeBatchRadioButtonList_1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_StatementCreateButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ClaimsType")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddClaimStatementButton"),10).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_btnReleaseBatch")).Click();
            try
            {
                Assert.AreEqual("Processed", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }

        [Test]
        public void TheStatements_Delete_Batch_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("A1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddBatchButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_ClaimTypeBatchRadioButtonList_1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_StatementCreateButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ClaimsType")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddClaimStatementButton"), 10).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_btnDeleteBatch"),10).Click();
            //Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure you want to delete this batch[\\s\\S](\n|\r\n)All claims in this batch will be permanently deleted\\.(\n|\r\n)This action cannot be undone\\.$"));
            driver.SwitchTo().Alert().Accept();
            //CloseAlertAndGetItsText();

            try
            {
                Assert.AreEqual("[Deleted]", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_BatchStatusName"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }

        [Test]
        public void TheStatements_Delete_Individual_Statement_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("A1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddBatchButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_ClaimTypeBatchRadioButtonList_1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_StatementCreateButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ClaimsType")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddClaimStatementButton"), 10).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("Rude!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"),10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_btnDeleteClaim"),10).Click();
            try
            {
                Assert.AreEqual("BLUTH, LINDSEY", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }

        [Test]
        public void TheStatements_Select_All_Statements_On_Page_And_Delete_Selected_Statements_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            driver.FindElement(By.Id("A1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddBatchButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_ClaimTypeBatchRadioButtonList_1"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_StatementCreateButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("George \"GOB\" Bluth JR");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("THANK YOU FOR THE MAGIC SHOW!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money though, medical procedures ain't free!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("JOB BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            try
            {
                Assert.AreEqual("Pending", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ClaimsType")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_AddClaimStatementButton"), 10).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S1_AcctNo")).SendKeys("123456789");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S21_PayBox1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S36_PayBox2")).SendKeys("09/09/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M2_RecName")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M5_RecAddr1")).SendKeys("14899 S 300 E");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M7_RecCity")).SendKeys("Orem");
            new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_ctl00_M8_RecSt"))).SelectByText("UT");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_M9_RecZip")).SendKeys("84057");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TopMsg")).SendKeys("Rude!");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_msgs")).SendKeys("Please pay us our money");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column2")).SendKeys("Physical");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column3")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column4")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column5")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column6")).SendKeys("50.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine1Column7")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_lbInsertLine")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1"), 10).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column1")).SendKeys("08/08/2013");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column2")).SendKeys("XRAY");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column3")).SendKeys("LINDSEY BLUTH");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column4")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column5")).SendKeys("0.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column6")).SendKeys("500.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_detailLines_tbLine2Column7")).SendKeys("450.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_Due2")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S39_Due1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_S44_Age1")).SendKeys("100.00");
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SaveBottom")).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("ctl00_MainContent_ctl00_SuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Click();
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_SelectAllClaimsButton"),10).Click();

            try
            {
                Assert.AreEqual("on", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_cb"),10).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("on", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl04_cb")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_DeleteSelectedClaimsButton")).Click();
            try
            {
                Assert.AreEqual("There are no claims associated with this batch", driver.FindElement(By.CssSelector("#ctl00_MainContent_ctl00_TrackClaims > tbody > tr > td"),10).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("A1")).Click();
            
            driver.FindElement(By.Id("track_link")).Click();

            endOfTest();
        }

        [Test]
        public void TheStatements_New_Statements_Upload_And_Edit_Test()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            while (!isUploaded)
            {
                isUploaded = Helper.UploadBatch(package);
            }
            driver.FindElement(By.Id("A1"), 10).Click();
            driver.Navigate().Refresh();
            int timeout = 0;
            bool isFound = false;
            isFailed = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_FailedLinkButton"));
            while (!isFound && !isFailed && timeout < 50)
            {
                isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton")); 
                if (!isFound)
                {
                    isFound = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"));
                    if (!isFound)
                    {
                        isFound =
                            driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"));   //Find Pending
                        if (!isFound)
                        {
                            driver.Navigate().Refresh();
                        }
                    }
                }
                timeout++;
            }
            if (isFailed)
            {
                verificationErrors.Append("Statement Batch Uploaded with an Unexpected Status of FAILED");
                Assert.Fail("Statement Batch Uploaded with an Unexpected Status of FAILED");
            }

            batch = driver.CaptureBatchNumberExternallyStatements();
            if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton")))
            {
                for (int second = 0; ; second++)
                {
                    if (second >= 90) Assert.Fail("timeout");
                    try
                    {
                        bool isDuplicate = driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_DuplicateLinkButton"));
                        
                        if (!isDuplicate)
                        {
                            if (
                                driver.isElementPresent(
                                    By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton"))) 
                            {
                                Helper.UpdateProcessedToReady(package, batch);
                                break;
                            }
                        }
                        else
                        {
                            Helper.UpdateDuplicates(batch, package);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    Thread.Sleep(1000);
                    driver.Navigate().Refresh();
                }
                driver.Navigate().Refresh();
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_PendingLinkButton"), 10).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_SelectAllClaimsButton"), 15)) break;
                    //if ("" == driver.FindElement(By.Id("ctl00_MainContent_ctl00_SelectAllClaimsButton"), 10).Text) break;  //Do we need this?
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            try
            {
                Assert.AreEqual("GLIDDEN, DONALD", driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"), 10).Text); //First Patient Listed
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackClaims_ctl03_PatientName"), 10).Click(); //First Patient Listed

            driver.FindElement(By.Id("McRecipName")).Clear();
            driver.FindElement(By.Id("McRecipName"), 10).SendKeys("DONALD GLIDDEN JR"); //Testing 3rd name CE
            try
            {
                Assert.AreEqual("07/12/2013", driver.FindElement(By.Id("McStatementDate"), 10).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            
            driver.FindElement(By.Id("McTopSaveFormButton"), 10).Click();
            try
            {
                Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("McSuccessLabel"), 10).Text);
            }
            catch (AssertionException e)
            {
                driver.Navigate().Refresh();
                try
                {
                    Assert.AreEqual("This statement has passed all edits and is ready for processing", driver.FindElement(By.Id("McSuccessLabel"), 10).Text);
                }
                catch (Exception)
                {
                    verificationErrors.Append(e.Message);
                }
            }

            driver.FindElement(By.Id("ctl00_BreadCrumbContent_TrackBreadCrumb_BatchNumButton"), 10).Click();

            driver.FindElement(By.Id("A1"),10).Click();
            /* The following segment is a temporary patch.  Batches for client QQM are going directly to a batch status of ACTIVE, which breaks
             * this test as it does not present a delete or release button in a status of active.  Will investigate.  Already editted provider record to wait
             */

            driver.FindElement(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_btnReleaseBatch"),10).Click();
            driver.Navigate().Refresh();

            if (!driver.isElementPresent(By.Id("ctl00_MainContent_ctl00_TrackBatch_ctl03_ProcessedLinkButton")))
            {
                verificationErrors.Append("Error");
            }

            endOfTest();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
