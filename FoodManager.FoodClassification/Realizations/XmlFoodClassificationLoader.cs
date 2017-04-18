using System.Linq;
using FoodManager.FoodClassification.Interface;
using System.IO;
using System.Xml;
using FoodManager.FoodClassification.Model;
using FoodManager.ServiceModels.FoodClassification;
using System.Collections.Generic;
using System;
using FoodManager.FoodClassification.Helpers;

namespace FoodManager.FoodClassification.Realizations
{
    public class XmlFoodClassificationLoader : IFoodClassificationLoader
    {
        private readonly string _xmlPath;

        public XmlFoodClassificationLoader(string xmlPath)
        {
            this._xmlPath = xmlPath;
        }

        public Model.FoodClassification Load()
        {
            string xmlString;
            using (StreamReader reader = new StreamReader(new FileStream(_xmlPath, FileMode.Open)))
            {
                xmlString = reader.ReadToEnd();
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var classification = new Model.FoodClassification();
            var classes = _DeserializeFoodClassification(xmlDoc.FirstChild.ChildNodes.Cast<XmlNode>(), null, classification);
            classification.FoodClasses = classes;

            return classification;
        }

        private IDictionary<string, FoodClass> _DeserializeFoodClassification(IEnumerable<XmlNode> nodes, FoodClass parent, Model.FoodClassification classification)
        {
            var foodClasses = new Dictionary<string, FoodClass>();

            foreach(var node in nodes)
            {
                var xmlNode = (XmlNode)node;
                var className = xmlNode.Attributes["name"].Value;
                var newFoodClass = new FoodClass(className);
                if(parent != null)
                {
                    newFoodClass.Parent = parent;
                }

                foodClasses.Add(className, newFoodClass);

                // storage attribute
                var storagePeriodAttributes = xmlNode.ChildNodes.Cast<XmlNode>()
                    .Where(n => n.Name == "attribute" && n.Attributes["type"].Value == "storagePeriod");

                foreach(var storPeriodAttr in storagePeriodAttributes)
                {
                    // period
                    var childPeriodNodesEnumerable = storPeriodAttr.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "period").ChildNodes.Cast<XmlNode>();

                    // from
                    var fromNode = childPeriodNodesEnumerable
                        .Single(n => n.Name == "from");
                    var fromQuantityNode = fromNode.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "quantity");
                    var fromQuantityValue = fromQuantityNode.LastChild.Value;
                    var fromUnitNode = fromNode.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "unit");
                    var fromUnitValue = fromUnitNode.LastChild.Value;

                    //to
                    var toNode = childPeriodNodesEnumerable
                        .Single(n => n.Name == "to");
                    var toQuantityNode = toNode.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "quantity");
                    var toQuantityValue = toQuantityNode.LastChild.Value;
                    var toUnitNode = toNode.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "unit");
                    var toUnitValue = toUnitNode.LastChild.Value;

                    // temperature
                    var childTemperatureNodesEnumerable = storPeriodAttr.ChildNodes.Cast<XmlNode>()
                        .Single(n => n.Name == "temperature").ChildNodes.Cast<XmlNode>();

                    // from
                    var fromTemperatureNode = childTemperatureNodesEnumerable
                        .Single(n => n.Name == "from");
                    var fromTemperatureValue = fromTemperatureNode.LastChild.Value;

                    //to
                    var toemperatureNode = childTemperatureNodesEnumerable
                        .Single(n => n.Name == "to");
                    var toTemperatureValue = toemperatureNode.LastChild.Value;

                    var storagePeriodInfo = new StoragePeriodInfo();
                    storagePeriodInfo.FromTime = UnitQuantityConverter.Convert(fromUnitValue, fromQuantityValue);
                    storagePeriodInfo.ToTime = UnitQuantityConverter.Convert(toUnitValue, toQuantityValue);
                    storagePeriodInfo.FromTemperature = int.Parse(fromTemperatureValue);
                    storagePeriodInfo.ToTemperature = int.Parse(toTemperatureValue);
                    storagePeriodInfo.DerivedFromClass = newFoodClass.Title;

                    newFoodClass.StoragePeriods.Add(storagePeriodInfo);
                }

                // storage advise
                var storageAdviseAttribute = xmlNode.ChildNodes.Cast<XmlNode>()
                    .SingleOrDefault(n => n.Name == "attribute" && n.Attributes["type"].Value == "storageAdvise");
                if(storageAdviseAttribute != null)
                {
                    var advice = storageAdviseAttribute.LastChild.Value;
                    newFoodClass.StorageAdvice = advice;
                }

                var childClasses = _DeserializeFoodClassification(xmlNode.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "class"),
                    newFoodClass, classification);
                newFoodClass.Childs = childClasses;
            }

            return foodClasses;
        }
    }
}
