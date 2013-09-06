using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace VendorUploadService
{
    class VendorDocument
    {
        private static string defaultPath = "0123456789.EOS";


        public DocumentType DocType { get; set; }
        public string Path { get; set; }
        public long? Length { get; set; }

        private VendorDocument(DocumentType DocType, string Path)
        {
            this.DocType = DocType;
            this.Path = Path;
        }

        private VendorDocument(DocumentType DocType, string Path, long Length)
        {
            this.DocType = DocType;
            this.Path = Path;
            this.Length = Length;
        }

        public static VendorDocument CreateDocument(DocumentType DocType, string path)
        {
            VendorDocument d = new VendorDocument(DocType, path);
            return d;
        }

        public static VendorDocument CreateDocument(DocumentType docType, string path, long length)
        {
            VendorDocument d = new VendorDocument(docType, path, length);
            return d;
        }

        internal static VendorDocument CreateDefaultDocument()
        {
            FileInfo file = new FileInfo(defaultPath);
            VendorDocument d = new VendorDocument(DocumentType.MedicalClaim, defaultPath, file.Length);
            return d;
        }
    }
}
