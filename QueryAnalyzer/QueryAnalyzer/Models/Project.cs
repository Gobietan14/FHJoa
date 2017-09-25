using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Models
{
    public class Project:BaseModel
    {
        public List<Issue> Issues { get; set; }
        public Credential Credential { get; set; }
        public int CredentialID { get; set; }

        public Project()
        {
            Issues = new List<Issue>();
            Credential = new Credential();
        }
    }
}
