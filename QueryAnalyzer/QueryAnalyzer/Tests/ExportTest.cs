using QueryAnalyzer.Backend;
using QueryAnalyzer.Backend.Export;
using Xunit;

namespace QueryAnalyzer.Tests
{
    public class ExportTest
    {

        [Fact]
        public void TestFactory()
        {
            var export = ExportFactory.GetExportTo("xlsx");
            Assert.True(export.GetType() == typeof(ExcelExport));
        }
    }
}
