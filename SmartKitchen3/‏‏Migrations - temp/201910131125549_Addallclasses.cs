namespace SmartKitchen3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addallclasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        CalendarId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.CalendarId);
            
            CreateTable(
                "dbo.ImportantDates",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        CalendatId = c.Int(nullable: false),
                        NotificationMessage = c.String(),
                        NotificationStartDateTime = c.DateTime(nullable: false),
                        Image = c.String(),
                        Calendar_CalendarId = c.Int(),
                    })
                .PrimaryKey(t => t.Date)
                .ForeignKey("dbo.Calendars", t => t.Calendar_CalendarId)
                .Index(t => t.Calendar_CalendarId);
            
            CreateTable(
                "dbo.UserCalendars",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CalendarId = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Calendars", t => t.CalendarId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.CalendarId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        KitchenId = c.Int(nullable: false),
                        Admin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Kitchens", t => t.KitchenId, cascadeDelete: true)
                .Index(t => t.KitchenId);
            
            CreateTable(
                "dbo.Kitchens",
                c => new
                    {
                        KitchenId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LocationLatitudes = c.Double(),
                        LocationLongitude = c.Double(),
                    })
                .PrimaryKey(t => t.KitchenId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Measure = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Recipe_RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId)
                .Index(t => t.IngredientId)
                .Index(t => t.Recipe_RecipeId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Instructions = c.String(),
                        ImageUrl = c.String(),
                        VideoUrl = c.String(),
                        Calories = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        KitchenId = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Kitchen_KitchenId = c.Int(),
                    })
                .PrimaryKey(t => t.KitchenId)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Kitchens", t => t.Kitchen_KitchenId)
                .Index(t => t.IngredientId)
                .Index(t => t.Kitchen_KitchenId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stocks", "Kitchen_KitchenId", "dbo.Kitchens");
            DropForeignKey("dbo.Stocks", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RecipeIngredients", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.UserCalendars", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "KitchenId", "dbo.Kitchens");
            DropForeignKey("dbo.UserCalendars", "CalendarId", "dbo.Calendars");
            DropForeignKey("dbo.ImportantDates", "Calendar_CalendarId", "dbo.Calendars");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Stocks", new[] { "Kitchen_KitchenId" });
            DropIndex("dbo.Stocks", new[] { "IngredientId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientId" });
            DropIndex("dbo.Users", new[] { "KitchenId" });
            DropIndex("dbo.UserCalendars", new[] { "User_UserId" });
            DropIndex("dbo.UserCalendars", new[] { "CalendarId" });
            DropIndex("dbo.ImportantDates", new[] { "Calendar_CalendarId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Stocks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Categories");
            DropTable("dbo.Kitchens");
            DropTable("dbo.Users");
            DropTable("dbo.UserCalendars");
            DropTable("dbo.ImportantDates");
            DropTable("dbo.Calendars");
        }
    }
}
