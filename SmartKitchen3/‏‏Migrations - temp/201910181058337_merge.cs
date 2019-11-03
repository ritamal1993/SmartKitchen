namespace SmartKitchen3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCalendars", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeIngredients", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Stocks", "Kitchen_KitchenId", "dbo.Kitchens");
            DropIndex("dbo.UserCalendars", new[] { "User_UserId" });
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.Stocks", new[] { "Kitchen_KitchenId" });
            DropColumn("dbo.UserCalendars", "UserId");
            DropColumn("dbo.RecipeIngredients", "RecipeId");
            DropColumn("dbo.Stocks", "KitchenId");
            RenameColumn(table: "dbo.UserCalendars", name: "User_UserId", newName: "UserId");
            RenameColumn(table: "dbo.RecipeIngredients", name: "Recipe_RecipeId", newName: "RecipeId");
            RenameColumn(table: "dbo.Stocks", name: "Kitchen_KitchenId", newName: "KitchenId");
            DropPrimaryKey("dbo.UserCalendars");
            DropPrimaryKey("dbo.RecipeIngredients");
            DropPrimaryKey("dbo.Stocks");
            AddColumn("dbo.UserCalendars", "UserCalendarId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.RecipeIngredients", "RecipeIngredientId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Stocks", "StockId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserCalendars", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserCalendars", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeIngredients", "RecipeId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecipeIngredients", "RecipeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Stocks", "KitchenId", c => c.Int(nullable: false));
            AlterColumn("dbo.Stocks", "KitchenId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserCalendars", "UserCalendarId");
            AddPrimaryKey("dbo.RecipeIngredients", "RecipeIngredientId");
            AddPrimaryKey("dbo.Stocks", "StockId");
            CreateIndex("dbo.UserCalendars", "UserId");
            CreateIndex("dbo.RecipeIngredients", "RecipeId");
            CreateIndex("dbo.Stocks", "KitchenId");
            AddForeignKey("dbo.UserCalendars", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RecipeIngredients", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "KitchenId", "dbo.Kitchens", "KitchenId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "KitchenId", "dbo.Kitchens");
            DropForeignKey("dbo.RecipeIngredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.UserCalendars", "UserId", "dbo.Users");
            DropIndex("dbo.Stocks", new[] { "KitchenId" });
            DropIndex("dbo.RecipeIngredients", new[] { "RecipeId" });
            DropIndex("dbo.UserCalendars", new[] { "UserId" });
            DropPrimaryKey("dbo.Stocks");
            DropPrimaryKey("dbo.RecipeIngredients");
            DropPrimaryKey("dbo.UserCalendars");
            AlterColumn("dbo.Stocks", "KitchenId", c => c.Int());
            AlterColumn("dbo.Stocks", "KitchenId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.RecipeIngredients", "RecipeId", c => c.Int());
            AlterColumn("dbo.RecipeIngredients", "RecipeId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserCalendars", "UserId", c => c.Int());
            AlterColumn("dbo.UserCalendars", "UserId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Stocks", "StockId");
            DropColumn("dbo.RecipeIngredients", "RecipeIngredientId");
            DropColumn("dbo.UserCalendars", "UserCalendarId");
            AddPrimaryKey("dbo.Stocks", "KitchenId");
            AddPrimaryKey("dbo.RecipeIngredients", "RecipeId");
            AddPrimaryKey("dbo.UserCalendars", "UserId");
            RenameColumn(table: "dbo.Stocks", name: "KitchenId", newName: "Kitchen_KitchenId");
            RenameColumn(table: "dbo.RecipeIngredients", name: "RecipeId", newName: "Recipe_RecipeId");
            RenameColumn(table: "dbo.UserCalendars", name: "UserId", newName: "User_UserId");
            AddColumn("dbo.Stocks", "KitchenId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.RecipeIngredients", "RecipeId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.UserCalendars", "UserId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Stocks", "Kitchen_KitchenId");
            CreateIndex("dbo.RecipeIngredients", "Recipe_RecipeId");
            CreateIndex("dbo.UserCalendars", "User_UserId");
            AddForeignKey("dbo.Stocks", "Kitchen_KitchenId", "dbo.Kitchens", "KitchenId");
            AddForeignKey("dbo.RecipeIngredients", "Recipe_RecipeId", "dbo.Recipes", "RecipeId");
            AddForeignKey("dbo.UserCalendars", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
