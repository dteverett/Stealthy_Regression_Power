using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace VendorUploadService
{
    public class VendorPackage : IPackage
    {
        //public static string defaultClientID = "ZZZ";
        //public static VendorDocument defaultDocument = VendorDocument.CreateDefaultDocument();
        //public static VendorPackage defaultPackage = VendorPackage.CreateDefaultPackage();

        //public static string currentClientID;
        //public static VendorDocument currentDocument;
        //public static VendorPackage currentPackage;

        public VendorPackage(Client client, Document doc)
        {
            this.Client = client;
            this.Document = doc;
        }

        public static VendorPackage createPackage(Client client, Document document)
        {
            VendorPackage p = new VendorPackage(client, document);
            return p;
        }

        //internal static void UpdatePackage()
        //{
        //    var p = new VendorPackage(Client.createClient(currentClientID == String.Empty ? defaultClientID : currentClientID),
        //        currentDocument ?? defaultDocument);

        //    currentPackage = p;
        //}

        public Document Document { get; set; }

        public Client Client { get; set; }
    }
}
