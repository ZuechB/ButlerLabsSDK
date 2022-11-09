using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButlerLabsSDK.Models
{
    public class UploadedDocumentResponse
    {
        public string uploadId { get; set; }
        public Document[] documents { get; set; }
    }

    public class Document
    {
        public string filename { get; set; }
        public string documentId { get; set; }
    }

}
