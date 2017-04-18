using FoodManager.Data.Models;

namespace FoodManager.BuisnessLogicService.Interface
{
    public interface IShoppingListService
    {
        ShoppingList GetUsersCurrentShoppingList(string userId);
        void AddProductToUsersCurrentShoppingList(FoodProduct foodProduct, string userId);
        void RemoveProductFromUsersCurrentShoppingList(int foodProductId, string userId);
    }
}
