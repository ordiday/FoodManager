using FoodManager.Data.Models;
using FoodManager.FoodClassification.Model;
using FoodManager.ServiceModels.FoodClassification;
using System.Collections.Generic;

namespace FoodManager.FoodClassification.Interface
{
    public interface IFoodClassificator
    {
        IDictionary<FridgeItem, IEnumerable<StoragePeriodInfo>> GetStoragePeriodAttributes(IEnumerable<FridgeItem> fridgeItems);
        IDictionary<FridgeItem, string> GetStorageAdviceAttributes(IEnumerable<FridgeItem> fridgeItems);
    }
}
