using QueryAnalyzer.Backend;
using QueryAnalyzer.Backend.Import;
using System;
using Xunit;

namespace QueryAnalyzer.Tests
{
    public class JiraTest
    {
        private static readonly Uri testUri = new Uri("https://queryexport.atlassian.net");

        public JiraTest()
        {
            
        }

        [Fact]
        public void TestFactory()
        {
            var jiraImport = ImportFactory.GetImportFrom("jira");
            Assert.True(jiraImport.GetType() == typeof(JiraImport));
        }

        [Fact]
        public void TestEmptyFactory()
        {
            //var import = ImportFactory.GetImportFrom(string.Empty);
            Exception ex = Assert.Throws<Exception>(() => ImportFactory.GetImportFrom(string.Empty));
            Assert.Equal("Unknown file type: !", ex.Message);
        }


        [Fact]
        public void TestConnect()
        {

        }
        
    }
}
