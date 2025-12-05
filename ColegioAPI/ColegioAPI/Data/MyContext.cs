using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
    }
}
