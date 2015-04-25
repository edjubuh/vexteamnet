namespace VexTeamNetwork.AccountMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("identity.AspNetUserClaims", "UserId", "identity.AspNetUsers");
            DropForeignKey("identity.AspNetUserLogins", "UserId", "identity.AspNetUsers");
            DropForeignKey("identity.AspNetUserRoles", "UserId", "identity.AspNetUsers");
            DropPrimaryKey("identity.AspNetUsers");
            AlterColumn("identity.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("identity.AspNetUsers", "Id");
            AddForeignKey("identity.AspNetUserClaims", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("identity.AspNetUserLogins", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("identity.AspNetUserRoles", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("identity.AspNetUserRoles", "UserId", "identity.AspNetUsers");
            DropForeignKey("identity.AspNetUserLogins", "UserId", "identity.AspNetUsers");
            DropForeignKey("identity.AspNetUserClaims", "UserId", "identity.AspNetUsers");
            DropPrimaryKey("identity.AspNetUsers");
            AlterColumn("identity.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("identity.AspNetUsers", "Id");
            AddForeignKey("identity.AspNetUserRoles", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("identity.AspNetUserLogins", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("identity.AspNetUserClaims", "UserId", "identity.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
