using FoodManager.FoodClassification.Realizations;
using FoodManager.ServiceModels.FoodClassification;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Tests
{
    public class FoodClassificatorTest
    {
        private readonly XmlFoodClassificationLoader _classificator;

        public FoodClassificatorTest()
        {
            var dir = Path.GetDirectoryName(typeof(FoodClassificatorTest).Assembly.Location);
            string xmlPath = Path.Combine(dir, "TestData", "xmlClassification.xml");
            this._classificator = new XmlFoodClassificationLoader(xmlPath);
        }

        [Test]
        public void TestLoad()
        {
            var actualClassification = _classificator.Load();

            var firstLevelClass1 = new FoodClass("FirstLevelClass1");
            var spAttr1 = new StoragePeriodInfo()
            {
                FromTime = TimeSpan.FromHours(48),
                ToTime = TimeSpan.FromDays(6 * 30),
                FromTemperature = 6,
                ToTemperature = 10,
                DerivedFromClass = "FirstLevelClass1"
            };
            var spAttr2 = new StoragePeriodInfo()
            {
                FromTime = TimeSpan.FromDays(2),
                ToTime = TimeSpan.FromDays(3),
                FromTemperature = 1,
                ToTemperature = 3,
                DerivedFromClass = "FirstLevelClass1"
            };
            firstLevelClass1.StoragePeriods = new List<StoragePeriodInfo>() { spAttr1, spAttr2 };
            var secondLevelClass11 = new FoodClass("SecondLevelClass11");
            secondLevelClass11.Parent = firstLevelClass1;
            var secondLevelClass12 = new FoodClass("SecondLevelClass12");
            secondLevelClass12.Parent = firstLevelClass1;
            firstLevelClass1.Childs.Add(secondLevelClass11.Title, secondLevelClass11);
            firstLevelClass1.Childs.Add(secondLevelClass12.Title, secondLevelClass12);

            var firstLevelClass2 = new FoodClass("FirstLevelClass2");
            var secondLevelClass21 = new FoodClass("SecondLevelClass21");
            secondLevelClass21.Parent = firstLevelClass2;
            firstLevelClass2.Childs.Add(secondLevelClass21.Title, secondLevelClass21);

            var expected = new FoodClassification.Model.FoodClassification();
            expected.FoodClasses.Add(firstLevelClass1.Title, firstLevelClass1);
            expected.FoodClasses.Add(firstLevelClass2.Title, firstLevelClass2);

            Assert.AreEqual(expected, actualClassification);
        }
    }
}
