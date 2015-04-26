using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VexTeamNetwork.AutomatedDownloader.Models;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.AutomatedDownloader
{
    class NetworkContext : DbContext
    {
        public NetworkContext()
            : base("NetworkConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        void UpdateLastModifiers()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Resources && (e.State == EntityState.Added || e.State == EntityState.Modified));
            var userId = "VtnBot";
            foreach (var entry in entries)
            {
                ((Resources)entry.Entity).LastModifiedTime = DateTime.Now;
                ((Resources)entry.Entity).LastModifierUserId = userId;
            }
        }

        public override int SaveChanges()
        {
            UpdateLastModifiers();
            try
            {
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateLastModifiers();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            UpdateLastModifiers();
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<AutomatedDownloader.Models.Team> Teams { get; set; }
    }
}
