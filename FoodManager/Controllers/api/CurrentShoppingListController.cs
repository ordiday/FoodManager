using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace FoodManager.Web.Controllers.api
{
    public class CurrentShoppingListController : ApiController
    {
        private readonly IShoppingListService _shoppingListService;

        public CurrentShoppingListController(IShoppingListService shoppingListService)
        {
            this._shoppingListService = shoppingListService;
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            var userId = User.Identity.GetUserId();
            var shoppingList = _shoppingListService.GetUsersCurrentShoppingList(userId);

            return Ok(shoppingList);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(FoodProduct foodProduct)
        {
            var userId = User.Identity.GetUserId();
            _shoppingListService.AddProductToUsersCurrentShoppingList(foodProduct, userId);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            _shoppingListService.RemoveProductFromUsersCurrentShoppingList(id, userId);

            return Ok();
        }
    }
}