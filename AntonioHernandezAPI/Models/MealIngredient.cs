using System.Text.Json.Serialization;

namespace AntonioHernandezAPI.Models
{
    public class MealIngredient
    {
        public int IdMeal { get; set; }
        public int IdIngredient { get; set; }
        public string? Measure { get; set; }
        [JsonIgnore]
        public Meal Meal { get; set; }
        [JsonIgnore]
        public Ingredient Ingredient { get; set; }

    }
}
