using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VexTeamNetwork.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
    }
}
