using FoodManager.BuisnessLogicService.Interface;
using FoodManager.BuisnessLogicService.Realization;
using FoodManager.Data;
using FoodManager.FoodClassification.Interface;
using FoodManager.FoodClassification.Realizations;
using FoodManager.YummlyApi.Interface;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Unity.WebApi;
using YummlyApi;

namespace FoodManager.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<IFridgeService, FridgeService>();
            container.RegisterType<IFoodProductService, FoodProductService>();
            container.RegisterType<IShoppingListService, ShoppingListService>();
            container.RegisterType<IRecipeService, RecipeService>();
            container.RegisterType<IYummlyApiClient, YummlyApiClient>();

            // foodclassification
            var path = AppDomain.CurrentDomain.BaseDirectory;
            container.RegisterType<IFoodClassificationLoader, XmlFoodClassificationLoader>(new InjectionConstructor(Path.Combine(path, "App_Data/classification.xml")));
            container.RegisterType<IFoodClassificator, FoodClassificator>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}