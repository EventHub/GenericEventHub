namespace GenericEventHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserWindowsName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "WindowsName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "WindowsName");
        }
    }
}
