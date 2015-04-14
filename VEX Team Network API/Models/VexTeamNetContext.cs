using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Api.Models
{
    public class VexTeamNetContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Team>().Configure();
            base.OnModelCreating(builder);
        }
    }
}