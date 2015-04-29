namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeams : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Teams", "TeamName", c => c.String());
            AddColumn("public.Teams", "LastModifier_UserId", c => c.String());
            AddColumn("public.Teams", "LastModifier_ModificiationTime", c => c.DateTime(nullable: false));
            DropColumn("public.Teams", "Name");
        }
        
        public override void Down()
        {
            AddColumn("public.Teams", "Name", c => c.String());
            DropColumn("public.Teams", "LastModifier_ModificiationTime");
            DropColumn("public.Teams", "LastModifier_UserId");
            DropColumn("public.Teams", "TeamName");
        }
    }
}
