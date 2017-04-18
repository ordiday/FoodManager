using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.FoodClassification.Helpers
{
    internal static class UnitQuantityConverter
    {
        internal static TimeSpan Convert(string unit, string value)
        {
            TimeSpan result = default(TimeSpan);
            switch (unit)
            {
                case "hour":
                    result = TimeSpan.FromHours(int.Parse(value));
                    break;
                case "day":
                    result = TimeSpan.FromDays(int.Parse(value));
                    break;
                case "week":
                    result = TimeSpan.FromDays(int.Parse(value) * 7);
                    break;
                case "month":
                    result = TimeSpan.FromDays(int.Parse(value) * 30);
                    break;
                case "year":
                    result = TimeSpan.FromDays(int.Parse(value) * 365);
                    break;
            }

            return result;
        }
    }
}
