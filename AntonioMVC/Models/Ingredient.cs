using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntonioMVC.Models
{
    public class Ingredient
    {
        [Key]
        public int IdIngredient { get; set; }
        public string Name { get; set; }
        public List<MealIngredient> MealIngredients { get; set; }
        [NotMapped]
        public List<int> MealsSeleccionados { get; set; }
    }
}