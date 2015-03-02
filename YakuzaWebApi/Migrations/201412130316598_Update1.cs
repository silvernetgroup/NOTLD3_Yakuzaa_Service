namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "ActiveSession_Id", newName: "GameSession_Id");
            RenameIndex(table: "dbo.Users", name: "IX_ActiveSession_Id", newName: "IX_GameSession_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_GameSession_Id", newName: "IX_ActiveSession_Id");
            RenameColumn(table: "dbo.Users", name: "GameSession_Id", newName: "ActiveSession_Id");
        }
    }
}
