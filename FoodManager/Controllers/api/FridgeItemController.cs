using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data;
using FoodManager.Data.Models;
using FoodManager.FoodClassification.Interface;
using FoodManager.Web.Helpers;
using FoodManager.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FoodManager.Web.Controllers.api
{
    public class FridgeItemController : ApiController
    {
        private readonly IFridgeService _fridgeService;
        private readonly IFoodClassificator _foodClassificator;

        public FridgeItemController(IFridgeService fridgeService, IFoodClassificator foodClassificator)
        {
            this._fridgeService = fridgeService;
            this._foodClassificator = foodClassificator;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<FridgeItemVm> Get()
        {
            string userId = User.Identity.GetUserId();
            var fridgeItems = _fridgeService.GetFridgeItemsByUserId(userId);

            var storagePeriodAttributes = _foodClassificator.GetStoragePeriodAttributes(fridgeItems);
            var storageAdviceAttributes = _foodClassificator.GetStorageAdviceAttributes(fridgeItems);

            var vms = ViewModelConvert.ConvertToFridgeVm(fridgeItems, storagePeriodAttributes, storageAdviceAttributes);

            return vms;
        }

        [HttpPost]
        [Authorize]
        public FridgeItem Post(FridgeItem item)
        {
            var userId = User.Identity.GetUserId();
            item = _fridgeService.AddProductToUsersFridge(item.FoodProductId, userId);

            return item;
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            _fridgeService.RemoveItemFromUsersFridge(id, userId);

            return Ok();
        }
    }
}