namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeams1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Teams", "LastModifierUserId", c => c.String());
            AddColumn("public.Teams", "LastModifiedTime", c => c.DateTime(nullable: false));
            DropColumn("public.Teams", "LastModifier_UserId");
            DropColumn("public.Teams", "LastModifier_ModificiationTime");
        }
        
        public override void Down()
        {
            AddColumn("public.Teams", "LastModifier_ModificiationTime", c => c.DateTime(nullable: false));
            AddColumn("public.Teams", "LastModifier_UserId", c => c.String());
            DropColumn("public.Teams", "LastModifiedTime");
            DropColumn("public.Teams", "LastModifierUserId");
        }
    }
}
