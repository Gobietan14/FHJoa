using QueryAnalyzer.Models;

namespace QueryAnalyzer.Backend.Export
{
    public interface IExport
    {
         byte[] Generate(Project data);
    }
}
