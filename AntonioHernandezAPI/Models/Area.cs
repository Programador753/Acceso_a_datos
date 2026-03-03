using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AntonioHernandezAPI.Models
{
    public class Area
    {
        [Key]
        public int IdArea { get; set; }
        public string Name { get; set; }
        public List<Meal> Meals { get; set; }
    }
}