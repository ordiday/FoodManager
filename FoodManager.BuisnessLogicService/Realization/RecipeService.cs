using System.Collections.Generic;
using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data.Models;
using FoodManager.Data;
using System.Linq;
using System.Data.Entity;
using FoodManager.YummlyApi.Interface;
using FoodManager.ServiceModels.Yummly;

namespace FoodManager.BuisnessLogicService.Realization
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IYummlyApiClient _yummlyClient;

        public RecipeService(ApplicationDbContext dbContext, IYummlyApiClient yummlyClient)
        {
            this._dbContext = dbContext;
            this._yummlyClient = yummlyClient;
        }

        public IEnumerable<Recipe> GetAppRecipesByUsersFridgeItems(string userId)
        {
            var dbFridgeItemsIds = _dbContext.FridgeItems.Where(fi => fi.UserId == userId).Select(fi => fi.FoodProductId).ToList();
            var dbRecipes = _dbContext.Recipes
                .Where(r => r.Products.All(p => dbFridgeItemsIds.Contains(p.Id)))
                .Include(r => r.Products)
                .Include(r => r.Steps)
                .ToList();

            return dbRecipes;
        }

        public YummlyGetRecipe GetYummlyRecipe(string yummlyId)
        {
            var recipe = _yummlyClient.GetRecipeById(yummlyId);

            return recipe;
        }

        public IEnumerable<YummlySearchRecipe> GetYummlyRecipesByUsersFridgeItems(string userId)
        {
            var userProductEngTitles = _dbContext.FridgeItems.Where(fi => fi.UserId == userId && fi.Product.EngTitle != null)
                .Select(fi => fi.Product.EngTitle)
                .ToList();

            var yummlyRecipes = _yummlyClient.GetRecipes(userProductEngTitles);

            return yummlyRecipes;
        }
    }
}
