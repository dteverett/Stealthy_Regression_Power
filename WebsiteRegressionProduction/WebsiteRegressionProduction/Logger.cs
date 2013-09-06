using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebsiteRegressionProduction
{
    static class Logger
    {
        private static string logFileName;
        private static string logFileLocation = @"C:\tmp\Logger";

        public static void logResults(System.Reflection.MethodBase method, Results result)
        {
            logResults(method,result, "");
        }

        public static void logResults(System.Reflection.MethodBase method, Results result, string message)
        {
            string stringMethod = "";
            bool isNullMethod = true;
            if (method != null)
            {
                stringMethod = method.ToString();
                isNullMethod = false;
            }
            var sb = new StringBuilder();
            string date = DateTime.Now.ToString();
            int index = date.IndexOf(" ");
            string subString = date.Substring(0, index);
            string nwDate = Regex.Replace(subString, "/", "");
            logFileName = "WebsiteRegressionProduction_TestCycle." + nwDate;
            string currentLogFile = logFileLocation + @"\" + logFileName;
            try
            {
                if (!Directory.Exists(logFileLocation))
                {
                    Directory.CreateDirectory(logFileLocation);
                }
                if (!File.Exists(currentLogFile))
                {
                    File.WriteAllText(currentLogFile,
                        "DATE-TIME\t\t\t\tACTION\t\t\tTEST CLASS\t\t\tTEST NAME\t\t\t\t\t\tTEST STATUS\t\tERROR MESSAGES\n\n",
                        Encoding.ASCII);
                }
                if (!isNullMethod)
                {
                    if (stringMethod.Contains("ConfigurationFiles"))
                    {
                        sb.Append("\n\n\n");
                    }
                }
                sb.Append(String.Format("{0} : Test Executed: {1} : {2} : {3} : {4}\n\n", DateTime.Now.ToString(),
                    method.ReflectedType.Name, method, result, message));
                using (var stream = File.AppendText(currentLogFile))
                {
                    stream.Write(sb.ToString());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
            catch (Exception e)
            {
                if (!Directory.Exists(logFileLocation))
                {
                    Directory.CreateDirectory(logFileLocation);
                }
                if (!File.Exists(currentLogFile))
                {
                    File.WriteAllText(currentLogFile,
                        "DATE-TIME\t\t\t\tACTION\t\t\tTEST CLASS\t\t\tTEST NAME\t\t\t\t\t\tTEST STATUS\t\tERROR MESSAGES\n\n",
                        Encoding.ASCII);
                }
                sb.Append("Exception Occured in the Logger: " + e.Message);
                using (var stream = File.AppendText(currentLogFile))
                {
                    stream.Write(sb.ToString());
                }
            }

        }
    }

    public enum Results
    {
        Pass,
        Fail,
        Inconclusive,
        Incomplete
    }
}
