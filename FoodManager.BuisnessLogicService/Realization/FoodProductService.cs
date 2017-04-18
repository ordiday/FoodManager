using System;
using System.Collections.Generic;
using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data.Models;
using FoodManager.Data;
using System.Linq;

namespace FoodManager.BuisnessLogicService.Realization
{
    public class FoodProductService : IFoodProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FoodProduct> SearchProduct(int amount)
        {
            return SearchProduct("", amount);
        }

        public IEnumerable<FoodProduct> SearchProduct(string prodName, int amount)
        {
            var query = _dbContext.FoodProducts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(prodName))
            {
                query = query.Where(p => p.Title.Contains(prodName));
            }

            query = query
                .OrderBy(p => p.Title)
                .Take(amount);

            return query.ToList();
        }
    }
}
