using System.ComponentModel.DataAnnotations;

namespace UniversidadAPI.DTOs
{
    // Para creación
    public class MateriaCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Nombre { get; set; }

        public int Creditos { get; set; } = 3;

       
        [StringLength(100)]
        public string Descripcion { get; set; }
    }

    public class MateriaReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Creditos { get; set; }
    }

    public class MateriaUpdateDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Creditos { get; set; }
    }
}
