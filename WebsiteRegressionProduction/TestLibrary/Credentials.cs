using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public static class Credentials
    {

        public static Client MedicalClient5010
        {
            get{return new Client("ZZZ", "demo1", "medical1");}
        }

        public static Client DentalClientECF
        {
            get{return new Client("ZZD", "demo2", "dental2");}
        }

        public static Client StatementClient
        {
            get{return new Client("QQM", "demo22", "apex09");}
        }

        public static Client DentalClient5010
        {
            get{return new Client("VED", "versasuitedental", "apex11");}
        }

        public static Client AutomationMedical01
        {
            get{return new Client("OUS", "automation", "Password1" );} //May need onboarding's help to get these client's configured: cannot log on to the web with client
        }

        public static Client AutomationMedical02
        {
            get { return new Client("TST", "automation", "Password1"); } //May need onboarding's help to get these client's configured: cannot log on to the web with client
        }
    }
}
