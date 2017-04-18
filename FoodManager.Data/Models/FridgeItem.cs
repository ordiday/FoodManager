using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodManager.Data.Models
{
    public class FridgeItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Product")]
        public int FoodProductId { get; set; }

        public DateTime? AddDate { get; set; }

        public FoodProduct Product { get; set; }

        public ApplicationUser User { get; set; }
    }
}
