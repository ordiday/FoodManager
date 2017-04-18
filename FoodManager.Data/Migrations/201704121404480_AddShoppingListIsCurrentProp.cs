namespace FoodManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShoppingListIsCurrentProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "IsCurrent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "IsCurrent");
        }
    }
}
