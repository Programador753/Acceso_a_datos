namespace ColegioAPI.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public int CursoID { get; set; }
        public string Sexo { get; set; }
        public DateTime Fnac { get; set; }
    }
}
