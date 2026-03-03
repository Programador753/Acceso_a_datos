using System.ComponentModel.DataAnnotations;

namespace AntonioHernandezAPI.Models
{
    public class Category
    {
        [Key] 
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public List<Meal> Meals { get; set; }
    }
}