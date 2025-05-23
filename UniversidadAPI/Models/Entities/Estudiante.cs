using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models.Entities
{
    [Table("estudiantes")]
    public class Estudiante
    {
        [Key] //Clave primaria
        [Column("id_estudiante")]
        public int Id {  get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        [StringLength(50, ErrorMessage ="El nombre no puede exceder 50 caracteres")]
        [Column("nombre", TypeName ="Varchar(50)")]
        public string Nombre {  get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        [Column("apellido", TypeName = "varchar(50)")]
        public string Apellido {  get; set; }

        [Required]
        [EmailAddress]
        [Column("email", TypeName = "varchar(100)")]
        public string Email {  get; set; }

        [Column("fecha_inscripcion")]
        public DateTime FechaInscripcion {  get; set; } = DateTime.Now;
            
        //Configurar desde el DbContext
        public virtual ICollection<Inscripcion> Inscripciones {  get; set; }
    }
}
