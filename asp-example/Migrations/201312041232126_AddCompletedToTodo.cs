namespace asp_example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompletedToTodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Completed", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todoes", "Completed");
        }
    }
}
