using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class Document
    {
        public DocumentType DocType { get; set; }
        public string Path { get; set; }
        public long? Length { get; set; }

        public static Document CreateNullDocument()
        {
            Document d = null;
            return d;
        }

        public static Document CreateDocument(DocumentType DocType, string path)
        {
            Document d = new Document(DocType, path, null);
            return d;
        }

        public static Document CreateDocument(DocumentType DocType, string path, long? Length)
        {
            Document d = new Document(DocType, path, Length);
            return d;
        }

        public static Document CreateDocument(string path)
        {
            Document d = new Document(DocumentType.MedicalClaim, path);
            return d;
        }

        private Document(DocumentType DocType, string Path, long? Length)
        {
            this.DocType = DocType;
            this.Path = Path;
            this.Length = Length;
        }

        private Document(DocumentType DocType, string Path)
        {
            this.DocType = DocType;
            this.Path = Path;
            this.Length = GetFileLength(Path);
        }


        public static long? GetFileLength(string path)
        {
            try
            {
                var file = new FileInfo(path);
                long length = file.Length;
                return length;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
