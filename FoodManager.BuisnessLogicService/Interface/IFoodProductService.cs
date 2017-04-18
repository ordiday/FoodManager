using FoodManager.Data.Models;
using System.Collections.Generic;

namespace FoodManager.BuisnessLogicService.Interface
{
    public interface IFoodProductService
    {
        IEnumerable<FoodProduct> SearchProduct(int amount);
        IEnumerable<FoodProduct> SearchProduct(string prodName, int amount);
    }
}
