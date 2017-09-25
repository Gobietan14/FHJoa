using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Backend.Export
{
    public static class ExportFactory
    {
        public static IExport GetExportTo(string fileType)
        {
            switch(fileType)
            {
                case "xlsx":
                    return new ExcelExport();
                default:
                    throw new Exception($"Unknown file type: {fileType}!");
            }
        }
    }
}
