using FoodManager.ServiceModels.FoodClassification;
using System.Collections.Generic;

namespace FoodManager.FoodClassification.Model
{
    public class FoodClassification
    {
        public IDictionary<string, FoodClass> FoodClasses { get; set; } = new Dictionary<string, FoodClass>();

        public override bool Equals(object obj)
        {
            var other = obj as FoodClassification;
            if(other == null)
            {
                return false;
            }

            if((other.FoodClasses ?? this.FoodClasses) == null)
            {
                return true;
            }
            else
            {
                foreach(var foodClassKv in this.FoodClasses)
                {
                    if (!other.FoodClasses.ContainsKey(foodClassKv.Key))
                    {
                        return false;
                    }

                    var otherClass = other.FoodClasses[foodClassKv.Key];
                    var thisClass = foodClassKv.Value;

                    if (!otherClass.Equals(thisClass))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
