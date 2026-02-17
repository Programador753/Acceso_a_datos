using AntonioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AntonioAPI.Data
{
    //Contiene los metodos que se encargan de realizar las operaciones CRUD a la base de datos,
    //utilizando el contexto para acceder a las tablas y realizar las consultas necesarias.
    public class BibliotecaRepositorio
    {
        public Contexto Context { get; set; }
        public BibliotecaRepositorio(Contexto contexto)
        {
            Context = contexto;
        }

        public async Task<List<Autor>> GetAutoresAsync()
        {
            return await Context.Autores
                .Include(a => a.pais)
                .Include(a => a.AutorLibros)
                .ThenInclude(a => a.libro)
                .Include(a => a.AutorPremios)
                .ThenInclude(a => a.premio)
                .ThenInclude(a => a.pais).ToListAsync();
        }

        public async Task<List<Autor>> GetAtoresById(int ID)
        {
            return await Context.Autores
                .Include(a => a.pais)
                .Include(a => a.AutorLibros)
                .ThenInclude(a => a.libro)
                .Include(a => a.AutorPremios)
                .ThenInclude(a => a.premio)
                .ThenInclude(a => a.pais)
                .Where(a => a.id == ID)
                .ToListAsync();
        }

        public async Task<Autor> AddAutorAsync(Autor autor)
        {
            Context.Autores.Add(autor);
            await Context.SaveChangesAsync();
            return autor;
        }

    }
}
