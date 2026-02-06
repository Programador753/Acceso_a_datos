using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.Modelos
{
    public class TablaDto
    {
        public List<string> Columns { get; set; }
        public List<List<string>> Rows { get; set; }
    }
}
