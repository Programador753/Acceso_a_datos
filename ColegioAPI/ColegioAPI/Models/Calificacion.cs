namespace ColegioAPI.Models
{
    public class Calificacion
    {
        public int ID { get; set; }
        public int convocatoria { get; set; }
        public int nota { get; set; }

        public virtual Asignatura? asignatura { get; set; } // <--- EL INTERROGANTE ES CLAVE
        public int asignaturaID { get; set; }

        public virtual Alumno? alumno { get; set; } // <--- AQUÍ TAMBIÉN
        public int alumnoID { get; set; }
    }
}
