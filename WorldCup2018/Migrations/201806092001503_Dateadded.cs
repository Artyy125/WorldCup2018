namespace WorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInput", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInput", "DateTime");
        }
    }
}
