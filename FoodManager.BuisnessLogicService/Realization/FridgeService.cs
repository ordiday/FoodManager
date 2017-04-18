using System;
using System.Collections.Generic;
using FoodManager.BuisnessLogicService.Interface;
using FoodManager.Data.Models;
using FoodManager.Data;
using System.Linq;
using System.Data.Entity;

namespace FoodManager.BuisnessLogicService.Realization
{
    public class FridgeService : IFridgeService
    {
        private readonly ApplicationDbContext _dbContext;

        public FridgeService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public FridgeItem AddProductToUsersFridge(int foodProductId, string userId)
        {
            var newFridgeItem = new FridgeItem
            {
                UserId = userId,
                FoodProductId = foodProductId,
                AddDate = DateTime.Now
            };

            _dbContext.FridgeItems.Add(newFridgeItem);
            _dbContext.SaveChanges();

            _dbContext.Entry(newFridgeItem).Reference(fi => fi.Product).Load();

            return newFridgeItem;
        }

        public IEnumerable<FridgeItem> GetFridgeItemsByUserId(string userId)
        {
            var query = _dbContext.FridgeItems.AsQueryable()
                .Where(fi => fi.UserId == userId)
                .OrderBy(fi => fi.AddDate)
                .Include(fi => fi.Product);

            return query.ToList();
        }

        public void RemoveItemFromUsersFridge(int id, string userId)
        {
            var item = _dbContext.FridgeItems.AsQueryable()
                .Where(fi => fi.Id == id && fi.UserId == userId).SingleOrDefault();

            if(item != null)
            {
                _dbContext.FridgeItems.Remove(item);
                _dbContext.SaveChanges();
            }
        }
    }
}
