using System;

namespace FoodManager.ServiceModels.FoodClassification
{
    public class StoragePeriodInfo
    {
        public string DerivedFromClass { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

        public int FromTemperature { get; set; }
        public int ToTemperature { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as StoragePeriodInfo;
            if(other == null)
            {
                return false;
            }

            if(this.DerivedFromClass != other.DerivedFromClass)
            {
                return false;
            }

            if((!this.ToTime.Equals(other.ToTime) || (!this.FromTime.Equals(other.FromTime))))
            {
                return false;
            }

            if ((!this.ToTemperature.Equals(other.ToTemperature) 
                || (!this.FromTemperature.Equals(other.FromTemperature))))
            {
                return false;
            }

            return true;
        }
    }
}
