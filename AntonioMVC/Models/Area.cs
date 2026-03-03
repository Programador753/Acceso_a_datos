using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Models
{
    public class Area
    {
        [Key]
        public int IdArea { get; set; }
        public string Name { get; set; }
    }
}