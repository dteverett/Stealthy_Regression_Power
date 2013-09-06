using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace OneTouchUploadProduction
{
    public class PackageFactory
    {
        public PackageFactory()
        {
        }

        public OneTouchUploadPackage createPackage(Client client, Document document)
        {
            OneTouchUploadPackage package = new OneTouchUploadPackage(client, document);
            return package;
        }

        public OneTouchUploadPackage createPackage(Client client, string Path, DocumentType DocType)
        {
            OneTouchUploadPackage package = new OneTouchUploadPackage(client, Path, DocType);
            return package;
        }
    }
}
