using GenericEventHub.Migrations;
using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GenericEventHub.Infrastructure
{
    public class GenericEventHubDb : DbContext
    {
        public GenericEventHubDb()
            : base("DevelopmentDb")
        {
            
        }
        DbSet<Activity> Activities { get; set; }
        DbSet<Event> Events {get;set;}
        DbSet<Guest> Guests { get; set; }

        DbSet<Location> Locations { get; set; }
        DbSet<Sport> Sports { get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GenericEventHubDb>(new CreateDatabaseIfNotExists<GenericEventHubDb>());
            Database.SetInitializer<GenericEventHubDb>(new MigrateDatabaseToLatestVersion<GenericEventHubDb, Configuration>());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}