using RazorPages.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.Services
{
    public class AlumnoRepositorio : IAlumnoRepositorio
    {
        public List<Alumno> listaAlumnos;
        public AlumnoRepositorio()
        {
            listaAlumnos = new List<Alumno>()
            {
                new Alumno() {Id= 1, Nombre="Víctor Archidona", CursoID=Curso.H2, Email="archidona@gmail.com", Foto="archidona.jpg"},
                new Alumno() {Id= 2, Nombre="Silvia Arnas", CursoID=Curso.H1, Email="arnas@gmail.com", Foto="arnas.jpg"},
                new Alumno() {Id= 3, Nombre="Diego Ballonga", CursoID=Curso.H2, Email="ballonga@gmail.com", Foto="ballonga.jpg"},
                new Alumno() {Id= 4, Nombre="Mario Bueno", CursoID=Curso.E2, Email="bueno@gmail.com", Foto="bueno.jpg"},
            };

        }

        public IEnumerable<Alumno> GetAllAlumnos()
        {
            return listaAlumnos;
        }

        public Alumno GetAlumno(int id)
        {
            return listaAlumnos.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Alumno alumnoActualizado)
        {
            Alumno alumno = listaAlumnos.FirstOrDefault(a => a.Id == alumnoActualizado.Id);
            alumno.Nombre = alumnoActualizado.Nombre;
            alumno.Email = alumnoActualizado.Email;
            alumno.CursoID = alumnoActualizado.CursoID;
            alumno.Foto = alumnoActualizado.Foto;
        }
    }
}
