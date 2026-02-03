namespace MVC26.Models
{
    public class VehiculoExtraModelo
    {
        public int ID { get; set; }
        public int vehiculoID { get; set; }
        public VehiculoModelo Vehiculo { get; set; }
        public int extraID { get; set; }
        public ExtraModelo Extra { get; set; }
    }
}
