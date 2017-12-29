namespace WorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Team1 = c.String(),
                        Team2 = c.String(),
                        Team1Score = c.Int(),
                        Team2Score = c.Int(),
                        Team1PenaltyScore = c.Int(),
                        Team2PenaltyScore = c.Int(),
                        Winner = c.String(),
                        Team1FlagUrl = c.String(),
                        Team2FlagUrl = c.String(),
                        MatchDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Match");
        }
    }
}
