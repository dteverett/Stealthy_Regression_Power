using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebsiteRegressionProduction
{
    [TestFixture]
    class ConfigurationFiles : Test
    {
        private string baseStructure = @"\\apexdata\data";
        private string prodDB = "APEXSQL";
        
        [SetUp]
        public void SetupTest()
        {
            verificationErrors = new StringBuilder();
            SetupTestGeneric();
        }

        [TearDown]
        public void TearDownTest()
        {
            TearDownTestGeneric();
        }

        [Test]
        public void TheConfigurationFilesTest()
        {
            method = new StackTrace().GetFrame(0).GetMethod();
            string connectionErrorMessage = "'s connection string is not configured to the correct database.\n";
            string dirErrorMessage = "'s directory paths are not configured to the correct network drives.\n";

            var regPrograms = new string[2] {/*"ApexWatcher",*/ "AutoImport5010", "Output5010"};  //Programs with regular names for config files, i.e. <Program>.exe.config, and also live in folders with their name, ie apexwatcher\apexwatcher.exe.config
            var appPrograms = new string[2] {"Claimstaker", "Claimstaker64"};    //Programs with app.configs
            var otherProgramsPlus = new string[2] {"ClaimstakerUI", "RunClaimstakerUI"}; //Programs that reside in the ClaimstakerPlus folder but have different names, i.e. ClaimstakerPlus\
            var otherProgramClassic = "RunClaimstaker";  //program that lives in the classic folder but has an exe.config extension

            string content;
            string path;
            bool connectionString;
            bool directoryPath;

            foreach (var config in regPrograms)
            {
                try
                {
                    path = baseStructure + @"\" + config + @"\" + config + ".exe.config";
                    content = File.ReadAllText(path);
                    connectionString = Regex.IsMatch(content, prodDB);
                    directoryPath = content.Contains(baseStructure);
                    if (!connectionString)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(connectionErrorMessage);
                    }
                    if (!directoryPath)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(dirErrorMessage);
                    }
                    content = "";
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                
            }

            connectionString = false;
            directoryPath = false;

            foreach (var config in appPrograms)
            {
                try
                {
                    path = baseStructure + @"\" + config + @"\" + "app.config";
                    content = File.ReadAllText(path);
                    connectionString = content.Contains(prodDB.ToLowerInvariant());
                    directoryPath = content.Contains(baseStructure);
                    //directoryPath = Regex.IsMatch(content, @"\\\\apexdata\\data");
                    if (!connectionString)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(connectionErrorMessage);
                    }
                    if (!directoryPath)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(dirErrorMessage);
                    }
                    content = "";
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }

            connectionString = false;
            directoryPath = false;

            foreach (var config in otherProgramsPlus)
            {
                try
                {
                    bool hasNoConnectionString = config.Contains("Run");
                    path = baseStructure + @"\ClaimstakerPlus\" + config + ".exe.config";
                    content = File.ReadAllText(path);
                    connectionString = content.Contains(prodDB);
                    directoryPath = content.Contains(baseStructure);
                    if (!connectionString && !hasNoConnectionString)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(connectionErrorMessage);
                    }
                    if (!directoryPath)
                    {
                        verificationErrors.Append(config);
                        verificationErrors.Append(dirErrorMessage);
                    }
                    content = "";
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }

            directoryPath = false;
            

            try
            {
                path = baseStructure + @"\Claimstaker\" + otherProgramClassic + ".exe.config";
                content = File.ReadAllText(path);
                directoryPath = content.Contains(baseStructure);
                if (!directoryPath)
                {
                    verificationErrors.Append(otherProgramClassic);
                    verificationErrors.Append(dirErrorMessage);
                }
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            endOfTest();
        }
    }
}
