using System.Collections.Generic;
using System.Linq;

namespace FoodManager.ServiceModels.FoodClassification
{
    public class FoodClass
    {
        public FoodClass(string title)
        {
            Title = title;
            StoragePeriods = new List<StoragePeriodInfo>();
            Childs = new Dictionary<string, FoodClass>();
        }

        public string Title { get; set; }
        public FoodClass Parent { get; set; }
        public IDictionary<string, FoodClass> Childs { get; set; }
        public IList<StoragePeriodInfo> StoragePeriods { get; set; }
        public string StorageAdvice { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as FoodClass;
            if(other == null)
            {
                return false;
            }

            if(this.Title != other.Title)
            {
                return false;
            }

            if(this.StoragePeriods.Any() && other.StoragePeriods.Any())
            {
                if(this.StoragePeriods.Count != other.StoragePeriods.Count)
                {
                    return false;
                }

                foreach(var sp in this.StoragePeriods)
                {
                    if(!other.StoragePeriods.Any(op => op.Equals(sp)))
                    {
                        return false;
                    }
                }
            }

            foreach(var childKv in this.Childs)
            {
                if (!other.Childs.ContainsKey(childKv.Key))
                {
                    return false;
                }

                var otherChild = other.Childs[childKv.Key];
                var thisChild = childKv.Value;

                if (!thisChild.Equals(otherChild))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
