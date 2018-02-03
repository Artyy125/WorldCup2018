namespace WorldCup2018.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConfirmedEmail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmedEmail");
        }
    }
}
