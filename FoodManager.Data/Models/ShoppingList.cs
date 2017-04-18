using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodManager.Data.Models
{
    public class ShoppingList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public bool IsCurrent { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<FoodProduct> Products { get; set; }
    }
}
