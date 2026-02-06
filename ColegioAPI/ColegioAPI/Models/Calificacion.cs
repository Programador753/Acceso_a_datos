namespace ColegioAPI.Models
{
    public class Calificacion
    {
        public int ID { get; set; }
        public int convocatoria { get; set; }
        public Asignatura asignatura { get; set; }
        public int asignaturaID { get; set; }
        public Alumno alumno { get; set; }
        public int alumnoID { get; set; }
        public int nota { get; set; }
    }
}
