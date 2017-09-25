using Microsoft.EntityFrameworkCore;

namespace QueryAnalyzer.Models
{
    public class QueryAnalyzerContext : DbContext
    {
        public QueryAnalyzerContext (DbContextOptions<QueryAnalyzerContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Credential> Credential { get; set; }
        public DbSet<Issue> Issue { get; set; } 
    }
}
