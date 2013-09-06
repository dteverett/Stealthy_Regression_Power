using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class Client
    {
        //public static Client MedicalClient5010;// = Client.createClient("ZZZ", "demo1", "medical1");
        //public static Client DentalClientECF;// = Client.createClient("ZZD", "demo2", "dental2");
        //public static Client StatementClient;// = Client.createClient("ZZZ", "demo1", "medical1");
        //public static Client DentalClient5010;// = Client.createClient("VED", "versasuitedental", "apex11");
        
        public Client(string clientID, string clientLogin, string clientPassword)
        {
            ClientID = clientID;
            Username = clientLogin;
            Password = clientPassword;
        }

        //public Client(string clientID)
        //{
        //    ClientID = clientID;
        //    TestUserCredentials_T creds = DatabaseCalls.GetLogonCredentials(clientID);
        //    Username = creds.UserName_VC;
        //    Password = creds.Password_VC;
        //}

        public static Client createClient(string clientID, string username, string password)
        {
            Client c = new Client(clientID, username, password);
            return c;
        }

        public static Client createClient(string clientID)
        {
            TestUserCredentials_T creds = DatabaseCalls.GetLogonCredentials(clientID);
            Client c = new Client(clientID, creds.UserName_VC, creds.Password_VC);
            return c;
        }

        public string ClientID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

}