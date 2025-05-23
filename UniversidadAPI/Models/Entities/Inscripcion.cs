using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models.Entities
{
    [Table("inscripciones")]   
    
    public class Inscripcion
    {
        [Key]
        [Column("id_inscripcion")]
        public int Id { get; set; }

        [Required]
        [Column("id_estudiante")]
        [ForeignKey("Estudiante")]
        public int EstudianteId { get; set; }

        [Required]
        [Column("id_materia")]
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }

        [Required]
        [Column("id_profesor")]
        [ForeignKey("Profesor")]
        public int ProfesorId { get; set; }

        [Required]
        [Column("semestre", TypeName = "varchar(20)")]
        public string Semestre { get; set; }

        [Column("fecha_inscripcion")]
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        // Propiedades de navegación
        public virtual Estudiante Estudiante { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual Profesor Profesor { get; set; }
    }
}
