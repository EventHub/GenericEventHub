namespace GenericEventHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DayOfWeek = c.String(nullable: false),
                        PreferredTime = c.Time(nullable: false, precision: 7),
                        LocationID = c.Int(nullable: false),
                        RequiredNumberOfPlayers = c.Int(),
                        RecommendedNumberOfPlayers = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ActivityID = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Activities", t => t.ActivityID, cascadeDelete: true)
                .Index(t => t.ActivityID);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HostID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        Host_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.GuestID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Host_UserID)
                .Index(t => t.EventID)
                .Index(t => t.Host_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EMail = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Event_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Event_EventID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Event_EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guests", "Host_UserID", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.UserEvents", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Guests", "EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "ActivityID", "dbo.Activities");
            DropForeignKey("dbo.Activities", "LocationID", "dbo.Locations");
            DropIndex("dbo.Guests", new[] { "Host_UserID" });
            DropIndex("dbo.UserEvents", new[] { "Event_EventID" });
            DropIndex("dbo.UserEvents", new[] { "User_UserID" });
            DropIndex("dbo.Guests", new[] { "EventID" });
            DropIndex("dbo.Events", new[] { "ActivityID" });
            DropIndex("dbo.Activities", new[] { "LocationID" });
            DropTable("dbo.UserEvents");
            DropTable("dbo.Users");
            DropTable("dbo.Guests");
            DropTable("dbo.Events");
            DropTable("dbo.Locations");
            DropTable("dbo.Activities");
        }
    }
}
