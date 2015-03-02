namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateYakuzaDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameSessionId = c.Int(nullable: false),
                        TextContent = c.String(),
                        Date = c.DateTime(nullable: false),
                        isOnlyForMafia = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameSessions", t => t.GameSessionId, cascadeDelete: true)
                .Index(t => t.GameSessionId);
            
            CreateTable(
                "dbo.GameSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        IsNight = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        Username = c.String(),
                        RoleName = c.String(),
                        isInMafia = c.Boolean(nullable: false),
                        isKilled = c.Boolean(nullable: false),
                        ActiveSession_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameSessions", t => t.ActiveSession_Id)
                .Index(t => t.ActiveSession_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ActiveSession_Id", "dbo.GameSessions");
            DropForeignKey("dbo.GameEvents", "GameSessionId", "dbo.GameSessions");
            DropIndex("dbo.Users", new[] { "ActiveSession_Id" });
            DropIndex("dbo.GameEvents", new[] { "GameSessionId" });
            DropTable("dbo.Users");
            DropTable("dbo.GameSessions");
            DropTable("dbo.GameEvents");
        }
    }
}
