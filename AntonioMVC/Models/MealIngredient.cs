namespace AntonioMVC.Models
{
    public class MealIngredient
    {
        public int IdMeal { get; set; }
        public int IdIngredient { get; set; }
        public string? Measure { get; set; }
        public Meal Meal { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}
