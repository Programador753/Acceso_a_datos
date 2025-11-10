using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.Modelos
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string nomAsignatura { get; set; }
        public int horas { get; set; }
        public string CodAsignatura { get; set; }
        public Curso cursoId { get; set; }
        public int profeID { get; set; }

        [ForeignKey("profeID")] 
        public Profesor Profesor { get; set; }

    }
}
