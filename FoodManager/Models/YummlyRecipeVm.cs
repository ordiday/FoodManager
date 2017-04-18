namespace FoodManager.Web.Models
{
    public class YummlyRecipeVm : RecipeVmBase
    {
        public override bool IsYummly { get; } = true;
        public string Url { get; set; }
        public string YummlyId { get; set; }
    }
}