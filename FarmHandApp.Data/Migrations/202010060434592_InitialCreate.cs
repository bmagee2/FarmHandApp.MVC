namespace FarmHandApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bulletin",
                c => new
                    {
                        BulletinId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BulletinTitle = c.String(nullable: false),
                        BulletinText = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.BulletinId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BulletinId = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Bulletin", t => t.BulletinId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BulletinId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Chore",
                c => new
                    {
                        ChoreId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ChoreName = c.String(nullable: false),
                        ChoreDescription = c.String(nullable: false),
                        Location = c.Int(nullable: false),
                        Animal = c.Int(nullable: false),
                        TimeOfDay = c.Int(nullable: false),
                        IsDaily = c.Boolean(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ChoreId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ChoreId = c.Int(nullable: false),
                        NoteTitle = c.String(nullable: false),
                        NoteText = c.String(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Chore", t => t.ChoreId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ChoreId);
            
            CreateTable(
                "dbo.ChoreUser",
                c => new
                    {
                        ChoreUserId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ChoreId = c.Int(),
                        ChoreIsComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChoreUserId)
                .ForeignKey("dbo.Chore", t => t.ChoreId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ChoreId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ChoreUser", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.ChoreUser", "ChoreId", "dbo.Chore");
            DropForeignKey("dbo.Chore", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Note", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Note", "ChoreId", "dbo.Chore");
            DropForeignKey("dbo.Bulletin", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Comment", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Comment", "BulletinId", "dbo.Bulletin");
            DropIndex("dbo.ChoreUser", new[] { "ChoreId" });
            DropIndex("dbo.ChoreUser", new[] { "UserId" });
            DropIndex("dbo.Note", new[] { "ChoreId" });
            DropIndex("dbo.Note", new[] { "UserId" });
            DropIndex("dbo.Chore", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comment", new[] { "BulletinId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Bulletin", new[] { "UserId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ChoreUser");
            DropTable("dbo.Note");
            DropTable("dbo.Chore");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Comment");
            DropTable("dbo.Bulletin");
        }
    }
}
