namespace WorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userinput : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInput",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        MatchId = c.Int(nullable: false),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        Winner = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInput");
        }
    }
}
