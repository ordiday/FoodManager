using FoodManager.Data.Models;
using System.Collections.Generic;

namespace FoodManager.Web.Models
{
    public class DbRecipeVm : RecipeVmBase
    {
        public int Id { get; set; }
        public override bool IsYummly { get; } = false;
        public string Explanation { get; set; }
        public ICollection<FoodProduct> Products { get; set; }
        public ICollection<RecipeStep> Steps { get; set; }
    }
}