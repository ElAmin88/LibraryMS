namespace LMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAgeToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "age", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String());
            AlterColumn("dbo.Users", "name", c => c.String());
            DropColumn("dbo.Users", "age");
        }
    }
}
