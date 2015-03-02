namespace YakuzaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addusernametogameevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameEvents", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameEvents", "Username");
        }
    }
}
