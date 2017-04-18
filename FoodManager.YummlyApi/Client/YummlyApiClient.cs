using FoodManager.ServiceModels.Yummly;
using FoodManager.YummlyApi.Interface;
using FoodManager.YummlyApi.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace YummlyApi
{
    public class YummlyApiClient : IYummlyApiClient
    {
        public YummlyGetRecipe GetRecipeById(string yummlyId)
        {
            var client = new HttpClient();
            var baseUrl = @"http://api.yummly.com/v1/api/recipe/" +
                yummlyId +
                @"?_app_id=2d3c6665&_app_key=f4d13d46197efd4f9c0bda97540e2302";

            var rawResult = client.GetAsync(baseUrl).Result.Content.ReadAsStringAsync().Result;
            var deserializedResult = JsonConvert.DeserializeObject<YummlyGetRecipeResultInternal>(rawResult);

            var recipe = new YummlyGetRecipe
            {
                SourceUrl = deserializedResult.source.sourceRecipeUrl
            };

            return recipe;
        }

        public IEnumerable<YummlySearchRecipe> GetRecipes(IEnumerable<string> ingridients)
        {
            var client = new HttpClient();
            var baseUrl = @"http://api.yummly.com/v1/api/recipes?_app_id=2d3c6665&_app_key=f4d13d46197efd4f9c0bda97540e2302";

            foreach (var ingridient in ingridients)
            {
                baseUrl = baseUrl + "&allowedIngredient[]=" + ingridient;
            }

            var result = client.GetAsync(baseUrl).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            var deserializedResult = JsonConvert.DeserializeObject<YummlyRecipeSearchResult>(content);

            return deserializedResult.Matches;
        }
    }
}
