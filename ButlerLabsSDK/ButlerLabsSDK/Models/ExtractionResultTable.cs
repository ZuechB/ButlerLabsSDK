namespace ButlerLabsSDK.Models
{
    public class ExtractionResultTable
    {
        public string tableName { get; set; }
        public string confidenceScore { get; set; }
        public System.Collections.Generic.List<ExtractionResultTableRow> rows { get; set; }
    }
}