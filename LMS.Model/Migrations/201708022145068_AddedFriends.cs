namespace LMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFriends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "User_ID", c => c.Int());
            CreateIndex("dbo.Users", "User_ID");
            AddForeignKey("dbo.Users", "User_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "User_ID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_ID" });
            DropColumn("dbo.Users", "User_ID");
        }
    }
}
