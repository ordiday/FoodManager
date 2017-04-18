using FoodManager.Data.Models;
using System.Collections.Generic;

namespace FoodManager.BuisnessLogicService.Interface
{
    public interface IFridgeService
    {
        IEnumerable<FridgeItem> GetFridgeItemsByUserId(string userId);
        FridgeItem AddProductToUsersFridge(int foodProductId, string userId);
        void RemoveItemFromUsersFridge(int id, string userId);
    }
}
