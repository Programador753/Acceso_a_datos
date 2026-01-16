namespace MVC26.Models
{
    public class MarcaModelo
    {
        public int ID { get; set; }
        public string Nom_Marca { get; set; }
        public List<SerieModelo> Series { get; set; }

    }
}
