using ClimateAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClimateAPI.Data.Context
{
    public class ClimateContext : DbContext
    {
        public ClimateContext(DbContextOptions<ClimateContext> options) : base(options)
        {

        }
        public DbSet<Climate>? Climates { get; set; }
        public DbSet<Subscriber>? Subscribers { get; set; }
    }
}
