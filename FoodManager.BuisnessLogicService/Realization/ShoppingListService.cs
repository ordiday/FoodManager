using System;
using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data;
using FoodManager.Data.Models;
using System.Linq;
using System.Data.Entity;

namespace FoodManager.BuisnessLogicService.Realization
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingListService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void AddProductToUsersCurrentShoppingList(FoodProduct foodProduct, string userId)
        {
            var curShoppList = _dbContext.ShoppingLists.AsQueryable()
                .Where(sl => sl.UserId == userId && sl.IsCurrent)
                .Include(sl => sl.Products)
                .Single();

            var dbFoodProduct = _dbContext.FoodProducts.Single(p => p.Id == foodProduct.Id);

            if(curShoppList.Products.All(p => p.Id != foodProduct.Id))
            {
                curShoppList.Products.Add(dbFoodProduct);
                _dbContext.SaveChanges();
            }
        }

        public ShoppingList GetUsersCurrentShoppingList(string userId)
        {
            var query = _dbContext.ShoppingLists.AsQueryable()
                .Where(sl => sl.UserId == userId && sl.IsCurrent)
                .Include(sl => sl.Products);

            return query.Single();
        }

        public void RemoveProductFromUsersCurrentShoppingList(int foodProductId, string userId)
        {
            var curShoppList = _dbContext.ShoppingLists.AsQueryable()
                .Where(sl => sl.UserId == userId && sl.IsCurrent)
                .Include(sl => sl.Products)
                .Single();

            var prodToDelete = curShoppList.Products.SingleOrDefault(p => p.Id == foodProductId);
            if (prodToDelete != null)
            {
                curShoppList.Products.Remove(prodToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
