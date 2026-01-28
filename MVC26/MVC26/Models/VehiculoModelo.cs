using System.ComponentModel.DataAnnotations.Schema;

namespace MVC26.Models
{
    public class VehiculoModelo
    {
        public int ID { get; set; }
        public string Matricula { get; set; }
        public string Color { get; set; }
        public SerieModelo Serie { get; set; }
        public int SerieID { get; set; }
        public List<VehiculoExtraModelo> VehiculosExtras { get; set; }
        [NotMapped]
        public List<int> ExtrasSeleccionados { get; set; }
    }
}
