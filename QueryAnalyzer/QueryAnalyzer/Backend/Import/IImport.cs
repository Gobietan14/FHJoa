using QueryAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Backend.Import
{
    public interface IImport
    {
        bool Connect(Credential credentials);
        Task<List<Issue>> GetIssuesForProject();
    }
}
