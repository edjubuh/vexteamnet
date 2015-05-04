namespace VexTeamNetwork.DbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitions",
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
                "dbo.Awards",
                c => new
                    {
                        CompetitionSku = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 128),
                        TeamNumber = c.String(nullable: false, maxLength: 6),
                        LastModifierUserId = c.String(),
                        LastModifiedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompetitionSku, t.Name, t.TeamNumber })
                .ForeignKey("dbo.Competitions", t => t.CompetitionSku, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamNumber, cascadeDelete: true)
                .Index(t => t.CompetitionSku)
                .Index(t => t.TeamNumber);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Number = c.String(nullable: false, maxLength: 6),
                        TeamName = c.String(),
                        RobotName = c.String(),
                        Organization = c.String(),
                        City = c.String(),
                        Region = c.String(),
                        Country = c.String(),
                        IsRegistered = c.Boolean(nullable: false),
                        Grade = c.Int(nullable: false),
                        Program = c.Int(nullable: false),
                        LastModifierUserId = c.String(),
                        LastModifiedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Sku = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 128),
                        LastModifierUserId = c.String(),
                        LastModifiedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sku, t.Name })
                .ForeignKey("dbo.Competitions", t => t.Sku, cascadeDelete: true)
                .Index(t => t.Sku);
            
            CreateTable(
                "dbo.Matches",
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
                .ForeignKey("dbo.Teams", t => t.Blue1Number)
                .ForeignKey("dbo.Teams", t => t.Blue2Number)
                .ForeignKey("dbo.Teams", t => t.Blue3Number)
                .ForeignKey("dbo.Teams", t => t.BlueSitNumber)
                .ForeignKey("dbo.Divisions", t => new { t.Sku, t.DivisionName }, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Red1Number)
                .ForeignKey("dbo.Teams", t => t.Red2Number)
                .ForeignKey("dbo.Teams", t => t.Red3Number)
                .ForeignKey("dbo.Teams", t => t.RedSitNumber)
                .Index(t => new { t.Sku, t.DivisionName })
                .Index(t => t.Red1Number)
                .Index(t => t.Red2Number)
                .Index(t => t.Red3Number)
                .Index(t => t.RedSitNumber)
                .Index(t => t.Blue1Number)
                .Index(t => t.Blue2Number)
                .Index(t => t.Blue3Number)
                .Index(t => t.BlueSitNumber);
            
            CreateTable(
                "dbo.QualifyingAwards",
                c => new
                    {
                        Award_Sku = c.String(nullable: false, maxLength: 16),
                        Award_Name = c.String(nullable: false, maxLength: 128),
                        Award_Team = c.String(nullable: false, maxLength: 6),
                        Competition_Sku = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => new { t.Award_Sku, t.Award_Name, t.Award_Team, t.Competition_Sku })
                .ForeignKey("dbo.Awards", t => new { t.Award_Sku, t.Award_Name, t.Award_Team }, cascadeDelete: true)
                .ForeignKey("dbo.Competitions", t => t.Competition_Sku, cascadeDelete: false)
                .Index(t => new { t.Award_Sku, t.Award_Name, t.Award_Team })
                .Index(t => t.Competition_Sku);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "RedSitNumber", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Red3Number", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Red2Number", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Red1Number", "dbo.Teams");
            DropForeignKey("dbo.Matches", new[] { "Sku", "DivisionName" }, "dbo.Divisions");
            DropForeignKey("dbo.Matches", "BlueSitNumber", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Blue3Number", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Blue2Number", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Blue1Number", "dbo.Teams");
            DropForeignKey("dbo.Divisions", "Sku", "dbo.Competitions");
            DropForeignKey("dbo.Awards", "TeamNumber", "dbo.Teams");
            DropForeignKey("dbo.QualifyingAwards", "Competition_Sku", "dbo.Competitions");
            DropForeignKey("dbo.QualifyingAwards", new[] { "Award_Sku", "Award_Name", "Award_Team" }, "dbo.Awards");
            DropForeignKey("dbo.Awards", "CompetitionSku", "dbo.Competitions");
            DropIndex("dbo.QualifyingAwards", new[] { "Competition_Sku" });
            DropIndex("dbo.QualifyingAwards", new[] { "Award_Sku", "Award_Name", "Award_Team" });
            DropIndex("dbo.Matches", new[] { "BlueSitNumber" });
            DropIndex("dbo.Matches", new[] { "Blue3Number" });
            DropIndex("dbo.Matches", new[] { "Blue2Number" });
            DropIndex("dbo.Matches", new[] { "Blue1Number" });
            DropIndex("dbo.Matches", new[] { "RedSitNumber" });
            DropIndex("dbo.Matches", new[] { "Red3Number" });
            DropIndex("dbo.Matches", new[] { "Red2Number" });
            DropIndex("dbo.Matches", new[] { "Red1Number" });
            DropIndex("dbo.Matches", new[] { "Sku", "DivisionName" });
            DropIndex("dbo.Divisions", new[] { "Sku" });
            DropIndex("dbo.Awards", new[] { "TeamNumber" });
            DropIndex("dbo.Awards", new[] { "CompetitionSku" });
            DropTable("dbo.QualifyingAwards");
            DropTable("dbo.Matches");
            DropTable("dbo.Divisions");
            DropTable("dbo.Teams");
            DropTable("dbo.Awards");
            DropTable("dbo.Competitions");
        }
    }
}
