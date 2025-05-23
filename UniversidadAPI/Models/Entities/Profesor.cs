using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models.Entities
{
    [Table("profesores")]
    public class Profesor
    {
        [Key]
        [Column("id_profesor")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        [Column("nombre", TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        [Column("apellido", TypeName = "varchar(50)")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; }

        // Relaciones
        public virtual ICollection<ProfesorMateria> MateriasDictadas { get; set; }
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }
}
