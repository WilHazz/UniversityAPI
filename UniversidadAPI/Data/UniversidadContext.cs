using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Models.Entities;

namespace UniversidadAPI.Data
{
    public class UniversidadContext : DbContext
    {
        //Constructor que recibe las opciones de configuración
        public UniversidadContext(DbContextOptions<UniversidadContext> options) : base(options) 
        {

        }

        // Tablas DbSets
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<ProfesorMateria> ProfesorMaterias { get; set; }

        // Método para configurar el modelo (relacion, restricciones)
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones para Inscripciones
            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Estudiante)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.EstudianteId);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Profesor)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.ProfesorId);

            // Restriccion: para controlar máximo 3 materias

            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.EstudianteId, i.Semestre });

            //Configuracion para profesor-materia (relacion muchos a muchos)
            modelBuilder.Entity<ProfesorMateria>()
                .HasKey(pm => new { pm.ProfesorId, pm.MateriaId });

            //Configuracion para Mysql
            modelBuilder.Entity<Estudiante>()
                .Property(e => e.FechaInscripcion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


        }

    }
}
