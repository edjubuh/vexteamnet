using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace VexTeamNetwork.Models
{
    public class NetworkContext : DbContext
    {
        public NetworkContext() : base("NetworkConnection")
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
            var userId = (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                ? HttpContext.Current.User.Identity.GetUserId()
                : "Anonymous";
            foreach (var entry in entries)
            {
                ((Resources)entry.Entity).LastModifiedTime = DateTime.Now;
                ((Resources)entry.Entity).LastModifierUserId = userId;
            }
        }

        public override int SaveChanges()
        {
            UpdateLastModifiers();
            return base.SaveChanges();
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

        public DbSet<Team> Teams { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Division> Divisions { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}