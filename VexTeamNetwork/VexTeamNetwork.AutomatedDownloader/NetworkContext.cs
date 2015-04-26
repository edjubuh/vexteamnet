using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VexTeamNetwork.AutomatedDownloader.Models;

namespace VexTeamNetwork.AutomatedDownloader
{
    class NetworkContext : DbContext
    {
        public NetworkContext() : base("NetworkConnection")
        {

        }

        public DbSet<Team> Teams { get; set; }
    }
}
