using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodManager.Data.Models;
using FoodManager.Web.Models;
using FoodManager.FoodClassification.Model;
using FoodManager.ServiceModels.FoodClassification;

namespace FoodManager.Web.Helpers
{
    internal static class ViewModelConvert
    {
        internal static IEnumerable<FridgeItemVm> ConvertToFridgeVm(IEnumerable<FridgeItem> fridgeItems, 
            IDictionary<FridgeItem, IEnumerable<StoragePeriodInfo>> storagePeriodAttributes,
            IDictionary<FridgeItem, string> storageAdviceAttributes)
        {
            var result = new List<FridgeItemVm>();
            foreach(var item in fridgeItems)
            {
                var vm = new FridgeItemVm
                {
                    Id = item.Id,
                    AddDate = item.AddDate,
                    FoodProductId = item.FoodProductId,
                    Product = item.Product,
                    UserId = item.UserId,
                };

                if (storagePeriodAttributes.ContainsKey(item))
                {
                    var spAttr = storagePeriodAttributes[item];
                    vm.StoragePeriods = spAttr.Select(sp => new StoragePeriodInfoVm
                    {
                        DerivedFromClass = sp.DerivedFromClass,
                        FromTime = sp.FromTime.TotalMilliseconds,
                        ToTime = sp.ToTime.TotalMilliseconds,
                        FromTemperature = sp.FromTemperature,
                        ToTemperature = sp.ToTemperature
                    });
                }

                if (storageAdviceAttributes.ContainsKey(item))
                {
                    var adviceAttr = storageAdviceAttributes[item];
                    vm.StorageAdvice = adviceAttr;
                }

                result.Add(vm);
            }

            return result;
        }
    }
}