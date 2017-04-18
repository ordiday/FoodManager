using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodManager.Data.Models
{
    public class RecipeStep
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Recipe Recipe { get; set; }
    }
}
