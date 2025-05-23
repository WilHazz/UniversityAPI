using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models.Entities
{
    [Table("materias")]
    public class Materia
    {
        [Key]
        [Column("id_materia")]
        public int Id { get; set; }

        [Required]
        [Column("nombre", TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        [Column("descripcion", TypeName = "text")]
        public string Descripcion { get; set; }

        [Column("creditos")]
        public int Creditos { get; set; } = 3;

        // Relaciones
        public virtual ICollection<ProfesorMateria> Profesores { get; set; } // relacion muchos a muchos
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }
}
