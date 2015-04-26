namespace VexTeamNetwork.NetworkMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMatches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Matches",
                c => new
                    {
                        Sku = c.String(nullable: false, maxLength: 16),
                        DivisionName = c.String(nullable: false, maxLength: 128),
                        Round = c.Int(nullable: false),
                        Instance = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Scheduled = c.DateTime(nullable: false),
                        Field = c.String(),
                        RedScore = c.Int(nullable: false),
                        BlueScore = c.Int(nullable: false),
                        OfficialScore = c.Boolean(nullable: false),
                        Red1Number = c.String(maxLength: 6),
                        Red2Number = c.String(maxLength: 6),
                        Red3Number = c.String(maxLength: 6),
                        RedSitNumber = c.String(maxLength: 6),
                        Blue1Number = c.String(maxLength: 6),
                        Blue2Number = c.String(maxLength: 6),
                        Blue3Number = c.String(maxLength: 6),
                        BlueSitNumber = c.String(maxLength: 6),
                    })
                .PrimaryKey(t => new { t.Sku, t.DivisionName, t.Round, t.Instance, t.Number })
                .ForeignKey("public.Teams", t => t.Blue1Number)
                .ForeignKey("public.Teams", t => t.Blue2Number)
                .ForeignKey("public.Teams", t => t.Blue3Number)
                .ForeignKey("public.Teams", t => t.BlueSitNumber)
                .ForeignKey("public.Divisions", t => new { t.Sku, t.DivisionName }, cascadeDelete: true)
                .ForeignKey("public.Teams", t => t.Red1Number)
                .ForeignKey("public.Teams", t => t.Red2Number)
                .ForeignKey("public.Teams", t => t.Red3Number)
                .ForeignKey("public.Teams", t => t.RedSitNumber)
                .Index(t => new { t.Sku, t.DivisionName })
                .Index(t => t.Red1Number)
                .Index(t => t.Red2Number)
                .Index(t => t.Red3Number)
                .Index(t => t.RedSitNumber)
                .Index(t => t.Blue1Number)
                .Index(t => t.Blue2Number)
                .Index(t => t.Blue3Number)
                .Index(t => t.BlueSitNumber);
            
            AlterColumn("public.Teams", "Grade", c => c.Int(nullable: false));
            AlterColumn("public.Teams", "Program", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("public.Matches", "RedSitNumber", "public.Teams");
            DropForeignKey("public.Matches", "Red3Number", "public.Teams");
            DropForeignKey("public.Matches", "Red2Number", "public.Teams");
            DropForeignKey("public.Matches", "Red1Number", "public.Teams");
            DropForeignKey("public.Matches", new[] { "Sku", "DivisionName" }, "public.Divisions");
            DropForeignKey("public.Matches", "BlueSitNumber", "public.Teams");
            DropForeignKey("public.Matches", "Blue3Number", "public.Teams");
            DropForeignKey("public.Matches", "Blue2Number", "public.Teams");
            DropForeignKey("public.Matches", "Blue1Number", "public.Teams");
            DropIndex("public.Matches", new[] { "BlueSitNumber" });
            DropIndex("public.Matches", new[] { "Blue3Number" });
            DropIndex("public.Matches", new[] { "Blue2Number" });
            DropIndex("public.Matches", new[] { "Blue1Number" });
            DropIndex("public.Matches", new[] { "RedSitNumber" });
            DropIndex("public.Matches", new[] { "Red3Number" });
            DropIndex("public.Matches", new[] { "Red2Number" });
            DropIndex("public.Matches", new[] { "Red1Number" });
            DropIndex("public.Matches", new[] { "Sku", "DivisionName" });
            AlterColumn("public.Teams", "Program", c => c.Int());
            AlterColumn("public.Teams", "Grade", c => c.Int());
            DropTable("public.Matches");
        }
    }
}
