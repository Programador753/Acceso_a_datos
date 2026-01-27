namespace MVC26.Models
{
    public class ExtraModelo
    {
        public int ID { get; set; }
        public string Nom_Extra { get; set; }
        public List<VehiculoExtraModelo> VehiculosExtras { get; set; }
        public List<int> VehiculoExtraID { get; set; }
    }
}
