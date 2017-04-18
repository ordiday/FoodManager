using FoodManager.Data.Models;
using FoodManager.ServiceModels;
using FoodManager.ServiceModels.Yummly;
using System.Collections.Generic;

namespace FoodManager.BuisnessLogicService.Interface
{
    public interface IRecipeService
    {
        IEnumerable<Recipe> GetAppRecipesByUsersFridgeItems(string userId);
        IEnumerable<YummlySearchRecipe> GetYummlyRecipesByUsersFridgeItems(string userId);
        YummlyGetRecipe GetYummlyRecipe(string yummlyId);
    }
}
