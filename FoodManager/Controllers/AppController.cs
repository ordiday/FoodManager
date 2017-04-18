using System.Web.Mvc;

namespace FoodManager.Web.Controllers
{
    public class AppController : Controller
    {
        [Authorize]
        public ActionResult Fridge()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult ShoppingLists()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Recipes()
        {
            return PartialView();
        }
    }
}