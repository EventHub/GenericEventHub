using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UltiSports.Models;

namespace UltiSports.Infrastructure
{
    public class UltiEventsContext : DbContext
    {
         public UltiEventsContext()
            : base("UltiSports2")
        {

        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<PlusOne> PlusOne { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Attendance> Attendance { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}