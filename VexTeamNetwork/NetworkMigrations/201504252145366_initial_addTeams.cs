namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_addTeams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Teams",
                c => new
                    {
                        Number = c.String(nullable: false, maxLength: 5),
                        Name = c.String(),
                        RobotName = c.String(),
                        Organization = c.String(),
                        City = c.String(),
                        Region = c.String(),
                        Country = c.String(),
                        IsRegistered = c.Boolean(nullable: false),
                        Grade = c.Int(),
                        Program = c.Int(),
                    })
                .PrimaryKey(t => t.Number);
            
        }
        
        public override void Down()
        {
            DropTable("public.Teams");
        }
    }
}
