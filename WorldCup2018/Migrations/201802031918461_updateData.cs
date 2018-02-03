namespace WorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInput", "Team1Score", c => c.Int());
            AlterColumn("dbo.UserInput", "Team2Score", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInput", "Team2Score", c => c.Int(nullable: false));
            AlterColumn("dbo.UserInput", "Team1Score", c => c.Int(nullable: false));
        }
    }
}
