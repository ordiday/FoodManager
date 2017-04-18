using FoodManager.ServiceModels.Yummly;

namespace FoodManager.YummlyApi.Model
{
    public class YummlyRecipeSearchResult
    {
        public YummlySearchRecipe[] Matches { get; set; }
    }
}
