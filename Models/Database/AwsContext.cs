using learning_asp_core.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace learning_asp_core.Models.Database
{
    public class AwsContext : DbContext
    {

        public DbSet<Workflow> Workflows { get; set; }

        public AwsContext(DbContextOptions options) : base(options)
        {

        }
    }
}
