using System.Collections.Generic;

namespace FoodManager.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string CreatorUserId { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Image { get; set; }
        public bool IsCreatedByUser { get; set; }

        public ICollection<FoodProduct> Products { get; set; }
        public ICollection<RecipeStep> Steps { get; set; }
    }
}
