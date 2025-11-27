using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HernandezCavero.Modelos
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Cliente { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
    }
}
