namespace MVC26.Models
{
    public class SerieModelo
    {
        public int ID { get; set; }
        public string Nom_Serie { get; set; }
        public MarcaModelo Marca { get; set; }
        public int MarcaID { get; set; }
    }
}
