using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace OneTouchUploadProduction
{
    public class OneTouchUploadPackage : IPackage
    {
        public Document Document { get; set; }
        public Client Client { get; set; }

        public OneTouchUploadPackage(Client Client, string Path, DocumentType DocType)
        {
            this.Client = Client;
            this.Document = Document.CreateDocument(DocType, Path, Document.GetFileLength(Path));
        }

        public OneTouchUploadPackage(Client Client, Document Document)
        {
            this.Client = Client;
            this.Document = Document;
        }

        public override string ToString()
        {
            return "Client ID: " + Client.ClientID + "\nPath: " + Document.Path + "\n Type: " + Document.DocType.ToString();
        }
    }
}
