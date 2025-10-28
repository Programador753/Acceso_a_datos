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
    public class AlumnoRepositorioDB : IAlumnoRepositorio
    {

        public ColegioDbContext Context { get; }
        public AlumnoRepositorioDB(ColegioDbContext context)
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
            IEnumerable<Alumno> consulta = Context.Alumnos;
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

        public IEnumerable<Alumno> Busqueda(string elementoABuscar)
        {
            if (string.IsNullOrEmpty(elementoABuscar))
            {
                return Context.Alumnos;
            }
            return Context.Alumnos.Where(a => a.Nombre.Contains(elementoABuscar) || a.Email.Contains(elementoABuscar));
        }
    }
}
