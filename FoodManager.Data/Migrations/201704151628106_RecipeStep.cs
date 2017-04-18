namespace FoodManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeStep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeSteps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        StepNumber = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            AddColumn("dbo.Recipes", "Image", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeSteps", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.RecipeSteps", new[] { "RecipeId" });
            DropColumn("dbo.Recipes", "Image");
            DropTable("dbo.RecipeSteps");
        }
    }
}
