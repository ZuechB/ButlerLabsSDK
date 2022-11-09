namespace ButlerLabsSDK.Models
{
    public class ExtractionResultItem
    {
        public string documentId { get; set; }
        public string documentStatus { get; set; }
        public string fileName { get; set; }
        public string mimeType { get; set; }
        public string documentType { get; set; }
        public string confidenceScore { get; set; }
        public System.Collections.Generic.List<ExtractionResultFormField> formFields { get; set; }
        public System.Collections.Generic.List<ExtractionResultTable> tables { get; set; }
    }
}