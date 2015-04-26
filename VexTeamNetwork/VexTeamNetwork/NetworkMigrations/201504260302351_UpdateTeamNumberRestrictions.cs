namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeamNumberRestrictions : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("public.Teams");
            AlterColumn("public.Teams", "Number", c => c.String(nullable: false, maxLength: 6));
            AddPrimaryKey("public.Teams", "Number");
        }
        
        public override void Down()
        {
            DropPrimaryKey("public.Teams");
            AlterColumn("public.Teams", "Number", c => c.String(nullable: false, maxLength: 5));
            AddPrimaryKey("public.Teams", "Number");
        }
    }
}
