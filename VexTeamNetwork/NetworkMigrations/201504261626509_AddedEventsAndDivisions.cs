namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventsAndDivisions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Competitions",
                c => new
                    {
                        Sku = c.String(nullable: false, maxLength: 16),
                        RobotEventsUrl = c.String(),
                        Program = c.Int(nullable: false),
                        Name = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Season = c.String(),
                        Venue = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Region = c.String(),
                        Postcode = c.String(),
                        Country = c.String(),
                        LastModifierUserId = c.String(),
                        LastModifiedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sku);
            
            CreateTable(
                "public.Divisions",
                c => new
                    {
                        Sku = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Sku, t.Name })
                .ForeignKey("public.Competitions", t => t.Sku, cascadeDelete: true)
                .Index(t => t.Sku);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Divisions", "Sku", "public.Competitions");
            DropIndex("public.Divisions", new[] { "Sku" });
            DropTable("public.Divisions");
            DropTable("public.Competitions");
        }
    }
}
