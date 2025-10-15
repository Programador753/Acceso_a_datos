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

        public void Add(Alumno alumnoNuevo)
        {
            alumnoNuevo.Id = listaAlumnos.Max(a => a.Id) + 1;
            listaAlumnos.Add(alumnoNuevo);

        }
        public void Delete(int id)
        {
            Alumno alumnoBorrar = listaAlumnos.FirstOrDefault(a => a.Id == id);
            if (alumnoBorrar != null)
            {
                listaAlumnos.Remove(alumnoBorrar);
            }
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

        public IEnumerable<CursoCuantos> AlumnoPorCurso(Curso? curso)
        {
            IEnumerable<Alumno> consulta = listaAlumnos;
            if (curso.HasValue)
            {
                consulta = consulta.Where(a => a.CursoID == curso);
            }
            return consulta.GroupBy(a => a.CursoID).Select(g => new CursoCuantos()
            {
               Clase = g.Key.Value,
               NumAlumnos = g.Count()
            });
        }
    }
}
