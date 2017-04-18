using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodManager.Web.Models
{
    public class StoragePeriodInfoVm
    {
        public string DerivedFromClass { get; set; }
        public double FromTime { get; set; }
        public double ToTime { get; set; }
        public int FromTemperature { get; set; }
        public int ToTemperature { get; set; }
    }
}