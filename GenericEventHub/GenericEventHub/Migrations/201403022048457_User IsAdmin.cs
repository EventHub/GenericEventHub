namespace GenericEventHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIsAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsAdmin");
        }
    }
}
