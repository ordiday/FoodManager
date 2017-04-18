using System.Collections.Generic;

namespace FoodManager.ServiceModels.Yummly
{
    public class YummlySearchRecipe
    {
        public string recipeName { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> imageUrlsBySize { get; set; }
    }
}
