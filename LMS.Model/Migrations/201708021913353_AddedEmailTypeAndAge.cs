namespace LMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailTypeAndAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "type", c => c.String());
            AddColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "email");
            DropColumn("dbo.Users", "type");
        }
    }
}
