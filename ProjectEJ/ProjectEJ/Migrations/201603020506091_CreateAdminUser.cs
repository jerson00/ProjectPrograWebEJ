namespace ProjectEJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAdminUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Is_Admin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Is_Admin");
        }
    }
}
