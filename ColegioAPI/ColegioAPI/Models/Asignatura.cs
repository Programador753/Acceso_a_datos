namespace ColegioAPI.Models
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string nomAsignatura { get; set; }
        public int horas { get; set; }
        public string CodAsignatura { get; set; }
        public int cursoId { get; set; }
        public Profesor profesor { get; set; }
        public int profesorID { get; set; }
    }
}
