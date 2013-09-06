using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;
//using WebRegressionTest;
using TestLibrary;


namespace OneTouchUploadProduction
{
    public class CallUploadService
    {
        public CallUploadService()
        {
        }

        private static Singleton singleton = Singleton.getInstance();

        public static bool upload(OneTouchUploadPackage package)
        {
            bool result = upload(package.Client, package.Document.Path, package.Document.DocType);
            return result;
        }

        public static bool upload(Client client, string path, DocumentType type)
        {
            //Allows client to trust all certificates.  DO NOT USE UNLESS CONNECTING TO INTERNAL SERVERS
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);


            var file = Path.GetFileName(path);
            var baseDir = Path.GetDirectoryName(path);


            var fullName = Path.Combine(baseDir, file);

            if (!File.Exists(fullName))
            {
                lock (Singleton.lockObject)
                {
                    if (!File.Exists(fullName))
                        File.Copy(path, fullName);
                }
            }

            //DatabaseCalls dataCalls = new DatabaseCalls();  # Went Static -dte


            string username, password;


            username = client.Username;
            password = client.Password;


            var webClient = new WebClient();
            webClient.QueryString.Add("username", username);
            webClient.QueryString.Add("password", password);
            webClient.QueryString.Add("isClaim", (type == DocumentType.DentalClaim || type == DocumentType.MedicalClaim) ? "true" : "false");
            webClient.QueryString.Add("DocumentType", type.ToString());

            bool isSuccessful = false;
            try
            {
                var url = ConfigurationManager.AppSettings["UploadURL"];
                
                if (url == null)
                {
                    url = "https://onetouch.apexedi.com/secure/OneTouchAPI.asmx/Upload";
                }

                var response = webClient.UploadFile(url, path);
                var status = Encoding.UTF8.GetString(response);

                isSuccessful = Regex.IsMatch(status.ToString(), "Upload Successful");

                Console.WriteLine(status, "Upload status");

                //Don't think we want the files to delete as new aren't being created as connex -DTE
                //lock(Singleton.lockObject2)
                //{
                //    File.Delete(path);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string error = ex.Message;

            }

            return isSuccessful;
        }
    }
}
