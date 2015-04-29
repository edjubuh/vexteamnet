namespace VexTeamNetwork.AccountMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstName : DbMigration
    {
        public override void Up()
        {
            AddColumn("identity.AspNetUsers", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("identity.AspNetUsers", "FirstName");
        }
    }
}
