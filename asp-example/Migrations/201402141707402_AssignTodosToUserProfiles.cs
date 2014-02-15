namespace asp_example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignTodosToUserProfiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "User_UserId", c => c.Int());
            AddForeignKey("dbo.Todoes", "User_UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Todoes", "User_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Todoes", new[] { "User_UserId" });
            DropForeignKey("dbo.Todoes", "User_UserId", "dbo.UserProfile");
            DropColumn("dbo.Todoes", "User_UserId");
        }
    }
}
