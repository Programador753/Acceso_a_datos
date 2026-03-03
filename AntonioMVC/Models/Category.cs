using System.ComponentModel.DataAnnotations;

namespace AntonioMVC.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string Name { get; set; }
    }
}