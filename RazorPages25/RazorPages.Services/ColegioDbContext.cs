using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPages.Modelos;

namespace RazorPages.Services
{
    public class ColegioDbContext : DbContext
    {
        public ColegioDbContext(DbContextOptions<ColegioDbContext> options) : base(options)
        {

        }
        public DbSet<Alumno> Alumnos {get; set;}
        public DbSet<Asignatura> Asignaturas { get; set; }
    }    
}
