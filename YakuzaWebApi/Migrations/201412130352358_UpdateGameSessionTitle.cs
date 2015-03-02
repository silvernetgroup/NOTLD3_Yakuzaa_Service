namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGameSessionTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameSessions", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSessions", "Title");
        }
    }
}
