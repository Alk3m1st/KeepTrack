namespace asp_example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisplayOrderToTodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todoes", "DisplayOrder");
        }
    }
}
