namespace UniversidadAPI.DTOs
{
    public class InscripcionCreateDTO
    {
        
        public int EstudianteId { get; set; }
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
        public string Semestre { get; set; }
    
    }

    public class InscripcionReadDTO
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; }
        public int ProfesorId { get; set; }
        public string ProfesorNombre { get; set; }
        public int MateriaId { get; set; }
        public string MateriaNombre { get; set; }
        public string Semestre { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
