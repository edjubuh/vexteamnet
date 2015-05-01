namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDivisionsToExtendResources : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Divisions", "LastModifierUserId", c => c.String());
            AddColumn("public.Divisions", "LastModifiedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Divisions", "LastModifiedTime");
            DropColumn("public.Divisions", "LastModifierUserId");
        }
    }
}
