namespace SmartKitchen3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEventModels2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeEvents",
                c => new
                    {
                        RecipeEventId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeEventId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        UserEventId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserEventId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeEvents", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            DropIndex("dbo.RecipeEvents", new[] { "RecipeId" });
            DropTable("dbo.UserEvents");
            DropTable("dbo.RecipeEvents");
        }
    }
}
