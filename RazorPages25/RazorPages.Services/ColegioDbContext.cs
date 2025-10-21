using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPages.Modelos;

namespace RazorPages.Services
{
    internal class ColegioDbContext : DbContext
    {
        protected ColegioDbContext(DbContextOptions<ColegioDbContext> options) : base(options)
        {

        }
        public DbSet<Alumno> Alumnos {get; set;}
    }
}
