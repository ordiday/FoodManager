using FoodManager.Data.Models;
using FoodManager.ServiceModels.FoodClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodManager.Web.Models
{
    public class FridgeItemVm
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int FoodProductId { get; set; }

        public DateTime? AddDate { get; set; }

        public FoodProduct Product { get; set; }

        public IEnumerable<StoragePeriodInfoVm> StoragePeriods { get; set; }
        public string StorageAdvice { get; internal set; }
    }
}