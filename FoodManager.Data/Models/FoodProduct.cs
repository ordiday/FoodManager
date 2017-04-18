using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodManager.Data.Models
{
    public class FoodProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string EngTitle { get; set; }
        public string Img { get; set; }
        public string ClassificationPath { get; set; }

        [JsonIgnore]
        public ICollection<FridgeItem> FridgeItems { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingList> ShoppingLists { get; set; }

        [JsonIgnore]
        public ICollection<Recipe> Recipes { get; set; }
    }
}
