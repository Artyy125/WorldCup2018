namespace WorldCup2018.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Password");
        }
    }
}
