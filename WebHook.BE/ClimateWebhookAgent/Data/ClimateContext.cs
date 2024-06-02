using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateWebhookAgent.Data
{
    public class ClimateContext : DbContext
    {
        public ClimateContext(DbContextOptions options):base(options){}
    }
}
