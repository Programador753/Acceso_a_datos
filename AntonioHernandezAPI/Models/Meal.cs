using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntonioHernandezAPI.Models
{
    public class Meal
    {
        [Key]
        public int IdMeal { get; set; }
        public string Name { get; set; }
        public int? IdCategory { get; set; }
        public int? IdArea { get; set; }
        public string? Instructions { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
        public string? YoutubeUrl { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public Area? Area { get; set; }
        public List<MealIngredient>? MealIngredients { get; set; }
        [NotMapped]
        public List<int>? IngredientsSeleccionados { get; set; }
    }
}