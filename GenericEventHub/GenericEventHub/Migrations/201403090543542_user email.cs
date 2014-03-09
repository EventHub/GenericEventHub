namespace GenericEventHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useremail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String());
        }
    }
}
