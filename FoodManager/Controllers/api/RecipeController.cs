using FoodManager.BuisnessLogicService.Interface;
using FoodManager.ServiceModels.Yummly;
using FoodManager.Web.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FoodManager.Web.Controllers.api
{
    public class RecipeController : ApiController
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this._recipeService = recipeService;
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            var userId = User.Identity.GetUserId();
            var dbRecipes = _recipeService.GetAppRecipesByUsersFridgeItems(userId);

            var yummlyRecipes = _recipeService.GetYummlyRecipesByUsersFridgeItems(userId);

            IEnumerable<RecipeVmBase> dbRecipeVms = dbRecipes.Select(r => new DbRecipeVm
            {
                Title = r.Title,
                Id = r.Id,
                Explanation = r.Explanation,
                Products = r.Products,
                Steps = r.Steps
            });

            var yummlyRecipeVms = yummlyRecipes.Select(r => new YummlyRecipeVm
            {
                Title = r.recipeName,
                YummlyId = r.id,
                ImgUrl = r.imageUrlsBySize["90"]
            });

            var resultVmList = dbRecipeVms.Concat(yummlyRecipeVms);

            return Ok(resultVmList);
        }

        [HttpGet]
        public YummlyGetRecipe Get(string yummlyId)
        {
            var recipe = _recipeService.GetYummlyRecipe(yummlyId);

            return recipe;
        }
    }
}