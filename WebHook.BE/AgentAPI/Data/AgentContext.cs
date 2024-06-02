using Microsoft.EntityFrameworkCore;

namespace AgentAPI.Data
{
    public class AgentContext : DbContext
    {
        public AgentContext(DbContextOptions<AgentContext> options) : base(options)
        {

        }
        public DbSet<Model.WebHook>? WebHooks { get; set; }
    }
}
