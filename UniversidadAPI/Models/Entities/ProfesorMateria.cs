using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models.Entities
{
    [Table("profesor_materia")]
    public class ProfesorMateria 
    {
        [Key]
        [Column("id_profesor_materia")]
        public int Id { get; set; }

        [Required]
        [Column("id_profesor")]
        [ForeignKey("Profesor")]
        public int ProfesorId { get; set; }

        [Required]
        [Column("id_materia")]
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }

        // Propiedades de navegación
        public virtual Profesor Profesor { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
