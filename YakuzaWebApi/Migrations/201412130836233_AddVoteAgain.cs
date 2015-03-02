namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VotedUsername = c.String(),
                        GameSessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Votes");
        }
    }
}
