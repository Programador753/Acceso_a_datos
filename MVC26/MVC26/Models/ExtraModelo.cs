using System.ComponentModel.DataAnnotations.Schema;

namespace MVC26.Models
{
    public class ExtraModelo
    {
        public int ID { get; set; }
        public string Nom_Extra { get; set; }
        public List<VehiculoExtraModelo> ExtraVehiculos { get; set; }
        [NotMapped]
        public List<int> VehiculoSeleccionados { get; set; }
    }
}
