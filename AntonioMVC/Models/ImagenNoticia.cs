namespace AntonioMVC.Models
{
    public class ImagenNoticia
    {
        public int Id { get; set; }
        public int NoticiaId { get; set; }
        public Noticia noticia { get; set; }
        public string UrlImagen { get; set; }
        public string Tipo { get; set; }
    }
}
