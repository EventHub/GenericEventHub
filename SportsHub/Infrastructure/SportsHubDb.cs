using System.Data.Entity;
using SportsHub.Models;

namespace SportsHub.Infrastructure
{
    public class SportsHubDb : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<PlusOne> PlusOne { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
    }
}