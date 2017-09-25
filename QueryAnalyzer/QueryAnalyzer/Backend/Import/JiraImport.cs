using Dapplo.Jira;
using Dapplo.Jira.Query;
using QueryAnalyzer.Backend.Import;
using QueryAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueryAnalyzer.Backend
{
    public class JiraImport : IImport
    {
        private IJiraClient client;

        public JiraImport()
        {

        }
        public bool Connect(Credential credentials)
        {
            client = JiraClient.Create(new Uri(credentials.Uri));
            client.SetBasicAuthentication(credentials.Username, credentials.Password);
            return true;
        }

        public async Task<List<Issue>>  GetIssuesForProject()
        {
            var test = await client.Issue.SearchAsync(Where.Text.Contains("Write"));
            //var test =   client.Issue.Issue;
            var result = new List<Issue>();
            foreach (var item in test)
            {
                result.Add(new Issue()
                {
                    Name = item.Key,
                    Description = item.Fields.Summary
                });
            }
            return result;
        }
    }
}
