using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VexTeamNetwork.Models;

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
            
            return base.SaveChanges();
        }

        public DbSet<Team> Teams { get; set; }
    }
}