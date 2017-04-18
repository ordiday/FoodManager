namespace FoodManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassificationPathForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodProducts", "ClassificationPath", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodProducts", "ClassificationPath");
        }
    }
}
