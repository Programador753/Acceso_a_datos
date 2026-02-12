namespace AntonioMVCv1.Models
{
    public class Premio
    {
        public int id { get; set; }
        public string nom_premio { get; set; }
        public int paisId { get; set; }
        public Pais pais { get; set; }
        public string tipo { get; set; }
    }
}
