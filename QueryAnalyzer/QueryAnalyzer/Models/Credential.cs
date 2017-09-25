using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Models
{
    public class Credential : BaseModel
    {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProjectKey { get; set; }
    }
}
