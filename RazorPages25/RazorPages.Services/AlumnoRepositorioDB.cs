using Microsoft.EntityFrameworkCore;
using RazorPages.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RazorPages.Services
{
    public class AlumnoRepositorioDB : IAlumnoRepositorio // Implementación de IAlumnoRepositorio usando Entity Framework Core 
    {

        public ColegioDbContext Context { get; } // Propiedad Context de tipo ColegioDbContext
        public AlumnoRepositorioDB(ColegioDbContext context) // Constructor que recibe un ColegioDbContext
        {
            Context = context;
        }
        public void Add(Alumno alumnoNuevo)
        {
            //Context.Alumnos.Add(alumnoNuevo);
            //Context.SaveChanges();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nombre", alumnoNuevo.Nombre),
                new SqlParameter("@Email", alumnoNuevo.Email),
                new SqlParameter("@Foto", alumnoNuevo.Foto),
                new SqlParameter("@CursoID", alumnoNuevo.CursoID)
            };

            Context.Database.ExecuteSqlRaw("EXEC InsertarAlumno @Nombre, @Email, @Foto, @CursoID", parameters);

        }
        public void Delete(int id)
        {
            Alumno alumnoBorrar = Context.Alumnos.Find(id);
            if (alumnoBorrar != null)
            {
                Context.Alumnos.Remove(alumnoBorrar);
                Context.SaveChanges();
            }
        }
        public IEnumerable<Alumno> GetAllAlumnos()
        {
            return Context.Alumnos.FromSqlRaw("Select * from Alumnos").ToList();
        }

        public Asignatura GetAsignatura(int ID)
        {
            return Context.Asignaturas.Find(ID);
        }
        public Alumno GetAlumno(int id)
        {
            //return Context.Alumnos.Find(id);
            SqlParameter parameter = new SqlParameter("@Id", id);
            return Context.Alumnos.FromSqlRaw<Alumno>("GetAlumnoById @Id", parameter)
                .ToList()
                .FirstOrDefault();
        }

        public void Update(Alumno alumnoActualizado)
        {
            var alumno = Context.Alumnos.Attach(alumnoActualizado);
            alumno.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public IEnumerable<CursoCuantos> AlumnoPorCurso(Curso? curso)
        {
            IEnumerable<Alumno> consulta = Context.Alumnos; // Obtener todos los alumnos
            if (curso.HasValue) // Verificar si se proporciona un curso
            {
                consulta = consulta.Where(a => a.CursoID == curso); // Filtrar por curso si se proporciona uno
            }
            return consulta.GroupBy(a => a.CursoID).Select(g => new CursoCuantos() // Proyección a CursoCuantos
            {
                Clase = g.Key.Value, // Accedemos al valor del enum Curso 
                NumAlumnos = g.Count() // Contamos el número de alumnos en cada grupo
            });
        }

        public IEnumerable<Alumno> Busqueda(string elementoABuscar)
        {
            if (string.IsNullOrEmpty(elementoABuscar))
            {
                return Context.Alumnos;
            }
            return Context.Alumnos.Where(a => a.Nombre.Contains(elementoABuscar) || a.Email.Contains(elementoABuscar));
        }

        public IEnumerable<Alumno> GetAlumnosCurso(Curso codigo) // Obtener alumnos por curso desde el repositorio 
        {
            return Context.Alumnos.Where(a => a.CursoID == codigo); // Funcion de LINQ para filtrar los alumnos por curso
        }
    }
}
