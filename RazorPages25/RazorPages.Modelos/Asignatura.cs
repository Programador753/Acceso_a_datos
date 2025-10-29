using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.Modelos
{
    internal class Asignatura
    {
        public int Id { get; set; }
        public string nomAsignatura { get; set; }
        public int horas { get; set; }
        public string CodAsignatura { get; set; }

    }
}
