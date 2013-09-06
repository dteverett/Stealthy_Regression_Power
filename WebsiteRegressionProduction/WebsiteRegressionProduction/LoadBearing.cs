using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneTouchUploadProduction;
using TestLibrary;
using VendorUploadService;

namespace WebsiteRegressionProduction
{
    class LoadBearing
    {
        private const string filesLocation = @"VendorFiles\LoadBearing";
        internal static void activateThreads(Client client)
        {
            ThreadPool.QueueUserWorkItem(x => UploadLoadBearingFiles(client));
        }

        internal static void UploadLoadBearingFiles(Client client)
        {
            string[] files = Directory.GetFiles(filesLocation);
            bool[] noThrownExceptions = new bool[files.Length];
            int[] numOfClaimResults = new int[files.Length];
            //int count = 0;
            //foreach (var file in files)
            //{
            //    var package = new VendorPackage(client, Document.CreateDocument(file));
            //    var results = UploadService.CallUploadService(package);
            //    if (results.claimResults == null)
            //    {
                    
            //    }
            //    if (results.noErrors && !results.thrownException)
            //    {
            //        noThrownExceptions[count] = true;
            //    }
            //    numOfClaimResults[count++] = results.claimResults.Length;
            //    incrementCounter();
            //}
        }

        private static void incrementCounter()
        {
            lock (Singleton.lockObject)
            {
                ++VendorAPI_Upload.FilesUploadedCounter;
            }
        }
    }

    class LoadRunner
    {
        public static void runLoadBearingVendor()
        {
            Thread t1 = new Thread(() => LoadBearing.activateThreads(Credentials.MedicalClient5010));
            Thread t2 = new Thread(() => LoadBearing.activateThreads(Credentials.AutomationMedical01));
            Thread t3 = new Thread(() => LoadBearing.activateThreads(Credentials.AutomationMedical02));

            t1.Start();
            t2.Start();
            t3.Start();
        }

        public static void testRunLoadBearingVendor(Client client)
        {
            LoadBearing.UploadLoadBearingFiles(client);
        }
    }
}
