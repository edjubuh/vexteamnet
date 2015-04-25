namespace VexTeamNetwork.AccountMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstName1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("identity.AspNetUsers", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("identity.AspNetUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
