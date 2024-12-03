using Microsoft.EntityFrameworkCore;
using learning_asp_core.Models.Requests.Inbound;


namespace learning_asp_core.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<CloseWorkflowRequest> StartOrderWorkflowRequests { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { 

        }
    }
}
