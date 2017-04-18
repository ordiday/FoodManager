using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.YummlyApi.Model
{
    public class YummlyGetRecipeResultInternal
    {
        public YummlySourceInfo source { get; set; }
    }

    public class YummlySourceInfo
    {
        public string sourceRecipeUrl { get; set; }
    }
}
