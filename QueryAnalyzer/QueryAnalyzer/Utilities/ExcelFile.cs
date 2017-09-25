using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace QueryAnalyzer.Utilities
{
    public class Status
    {
        public string Message { get; set; }
        public bool Success { get => string.IsNullOrWhiteSpace(Message); }
    }

    public class ExcelFile
    {
        public Status Status { get; set; }
        public Column ColumnConfiguration{ get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> DataRows { get; set; }
        public string DocumentName { get; set; }

        public ExcelFile()
        {
            Status = new Status();
            Headers = new List<string>();
            DataRows = new List<List<string>>();
        }
        
    }
}
