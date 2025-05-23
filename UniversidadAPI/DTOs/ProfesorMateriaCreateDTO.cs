namespace UniversidadAPI.DTOs
{
    public class ProfesorMateriaCreateDTO
    {
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
    }

    public class ProfesorMateriaReadDTO
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string ProfesorNombre { get; set; }
        public int MateriaId { get; set; }
        public string MateriaNombre { get; set; }
    }
}
