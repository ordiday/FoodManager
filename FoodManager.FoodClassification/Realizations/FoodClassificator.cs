using System;
using System.Collections.Generic;
using FoodManager.Data.Models;
using FoodManager.FoodClassification.Interface;
using FoodManager.FoodClassification.Model;
using FoodManager.ServiceModels.FoodClassification;
using System.Linq;

namespace FoodManager.FoodClassification.Realizations
{
    public class FoodClassificator : IFoodClassificator
    {
        private FoodClassification.Model.FoodClassification _foodClassification;
        private readonly IFoodClassificationLoader _classificationLoader;

        private FoodClassification.Model.FoodClassification FoodClassification
        {
            get
            {
                if (_foodClassification == null)
                {
                    _foodClassification = _classificationLoader.Load();
                }

                return _foodClassification;
            }
        }

        public FoodClassificator(IFoodClassificationLoader classificationLoader)
        {
            this._classificationLoader = classificationLoader;
        }

        public IDictionary<FridgeItem, IEnumerable<StoragePeriodInfo>> GetStoragePeriodAttributes(IEnumerable<FridgeItem> fridgeItems)
        {
            var result = new Dictionary<FridgeItem, IEnumerable<StoragePeriodInfo>>();
            
            foreach(var fridgeItem in fridgeItems)
            {
                if(fridgeItem.Product.ClassificationPath != null)
                {
                    var path = fridgeItem.Product.ClassificationPath;
                    var splittedPath = path.Split(new string[] { "&&" }, StringSplitOptions.None);

                    FoodClass foodClass = null;
                    var foodClassChain = new Queue<FoodClass>();
                    IDictionary<string, FoodClass> curClassSet = FoodClassification.FoodClasses;
                    foreach(var pathPart in splittedPath)
                    {
                        foodClass = curClassSet[pathPart];
                        foodClassChain.Enqueue(foodClass);
                        curClassSet = foodClass.Childs;
                    }

                    IEnumerable<StoragePeriodInfo> spInfo = null;
                    while(foodClassChain.Count != 0)
                    {
                        var fClass = foodClassChain.Dequeue();
                        if(fClass.StoragePeriods.Any())
                        {
                            spInfo = fClass.StoragePeriods;
                        }
                    }

                    if(spInfo != null)
                    {
                        result.Add(fridgeItem, spInfo);
                    }
                }
            }

            return result;
        }

        public IDictionary<FridgeItem, string> GetStorageAdviceAttributes(IEnumerable<FridgeItem> fridgeItems)
        {
            var result = new Dictionary<FridgeItem, string>();

            foreach (var fridgeItem in fridgeItems)
            {
                if (fridgeItem.Product.ClassificationPath != null)
                {
                    var path = fridgeItem.Product.ClassificationPath;
                    var splittedPath = path.Split(new string[] { "&&" }, StringSplitOptions.None);

                    FoodClass foodClass = null;
                    var foodClassChain = new Queue<FoodClass>();
                    IDictionary<string, FoodClass> curClassSet = FoodClassification.FoodClasses;
                    foreach (var pathPart in splittedPath)
                    {
                        foodClass = curClassSet[pathPart];
                        foodClassChain.Enqueue(foodClass);
                        curClassSet = foodClass.Childs;
                    }

                    string advice = null;
                    while (foodClassChain.Count != 0)
                    {
                        var fClass = foodClassChain.Dequeue();
                        if (fClass.StorageAdvice != null)
                        {
                            advice = fClass.StorageAdvice;
                        }
                    }

                    if (advice != null)
                    {
                        result.Add(fridgeItem, advice);
                    }
                }
            }

            return result;
        }
    }
}
