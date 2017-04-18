namespace FoodManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgToFoodProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        EngTitle = c.String(unicode: false),
                        Img = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FridgeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        FoodProductId = c.Int(nullable: false),
                        AddDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodProducts", t => t.FoodProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FoodProductId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorUserId = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        Explanation = c.String(unicode: false),
                        IsCreatedByUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ShoppingListFoodProducts",
                c => new
                    {
                        ShoppingList_Id = c.Int(nullable: false),
                        FoodProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_Id, t.FoodProduct_Id })
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodProducts", t => t.FoodProduct_Id, cascadeDelete: true)
                .Index(t => t.ShoppingList_Id)
                .Index(t => t.FoodProduct_Id);
            
            CreateTable(
                "dbo.RecipeFoodProducts",
                c => new
                    {
                        Recipe_Id = c.Int(nullable: false),
                        FoodProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_Id, t.FoodProduct_Id })
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodProducts", t => t.FoodProduct_Id, cascadeDelete: true)
                .Index(t => t.Recipe_Id)
                .Index(t => t.FoodProduct_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RecipeFoodProducts", "FoodProduct_Id", "dbo.FoodProducts");
            DropForeignKey("dbo.RecipeFoodProducts", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.FridgeItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingListFoodProducts", "FoodProduct_Id", "dbo.FoodProducts");
            DropForeignKey("dbo.ShoppingListFoodProducts", "ShoppingList_Id", "dbo.ShoppingLists");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FridgeItems", "FoodProductId", "dbo.FoodProducts");
            DropIndex("dbo.RecipeFoodProducts", new[] { "FoodProduct_Id" });
            DropIndex("dbo.RecipeFoodProducts", new[] { "Recipe_Id" });
            DropIndex("dbo.ShoppingListFoodProducts", new[] { "FoodProduct_Id" });
            DropIndex("dbo.ShoppingListFoodProducts", new[] { "ShoppingList_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ShoppingLists", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FridgeItems", new[] { "FoodProductId" });
            DropIndex("dbo.FridgeItems", new[] { "UserId" });
            DropTable("dbo.RecipeFoodProducts");
            DropTable("dbo.ShoppingListFoodProducts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Recipes");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FridgeItems");
            DropTable("dbo.FoodProducts");
        }
    }
}
