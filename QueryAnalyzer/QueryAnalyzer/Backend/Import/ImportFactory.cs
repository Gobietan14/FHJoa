using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Backend.Import
{
    public class ImportFactory
    {
        public static IImport GetImportFrom(string origin)
        {
            switch (origin)
            {
                case "jira":
                    return new JiraImport();
                default:
                    throw new Exception($"Unknown file type: {origin}!");
            }
        }
    }
}
