using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryAnalyzer.Models
{
    public class Issue : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }

    }
}
