using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.DTOs;
using UniversidadAPI.Models.Entities;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Services.Implementations
{
    public class InscripcionService : IInscripcionService
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public InscripcionService(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InscripcionReadDTO> CreateAsync(InscripcionCreateDTO inscripcionDto)
        {
            // Verficar si ya existe una inscripción de ese estudiante con ese profesor

            bool yaInscritoConProfesor = await _context.Inscripciones
                .AnyAsync(i => i.EstudianteId == inscripcionDto.EstudianteId &&
                               i.ProfesorId == inscripcionDto.ProfesorId);

            if (yaInscritoConProfesor)
            {
                throw new InvalidOperationException("El estudiante ya esta inscrito con este profesor en otra materia.");
            }

            // Validar si el estudiante ya esta inscrito en la misma materia 

            bool yaInscritoMateria = await _context.Inscripciones
                .AnyAsync(i => i.EstudianteId == inscripcionDto.EstudianteId &&
                               i.EstudianteId == inscripcionDto.EstudianteId);

            if (yaInscritoMateria)
            {
                throw new InvalidOperationException("El estudiante ya está inscrito en esta materia.");
            }

            //Crea la inscripción si pasa las validaciones
            var inscripcion = new Inscripcion
            {
                EstudianteId = inscripcionDto.EstudianteId,
                ProfesorId = inscripcionDto.ProfesorId,
                MateriaId = inscripcionDto.MateriaId,
                Semestre = inscripcionDto.Semestre,
                FechaInscripcion = DateTime.Now
            };

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            // relaciones para el DTO
            await _context.Entry(inscripcion).Reference(i => i.Estudiante).LoadAsync();
            await _context.Entry(inscripcion).Reference(i => i.Profesor).LoadAsync();
            await _context.Entry(inscripcion).Reference(i => i.Materia).LoadAsync();

            return _mapper.Map<InscripcionReadDTO>(inscripcion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
                return false;

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InscripcionReadDTO>> GetAllAsync()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Profesor)
                .Include(i => i.Materia)
                .ToListAsync();

            return inscripciones.Select(_mapper.Map<InscripcionReadDTO>);
        }

        public async Task<InscripcionReadDTO?> GetByIdAsync(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Profesor)
                .Include(i => i.Materia)
                .FirstOrDefaultAsync(i => i.Id == id);

            return inscripcion == null ? null : _mapper.Map<InscripcionReadDTO>(inscripcion);
        }

        public async Task<IEnumerable<EstudianteReadDTO>> GetCompanerosDeClaseAsync(int estudianteId)
        {
            //Buscar inscripciones del estudiante

            var inscripcionesDelEstudiante = await _context.Inscripciones
                .Where(i => i.EstudianteId == estudianteId)
                .ToListAsync();

            var companeros = new List<Estudiante>();

            foreach (var inscripcion in inscripcionesDelEstudiante)
            {
                var estudiantesMismaClase = await _context.Inscripciones
                    .Where(i =>
                        i.MateriaId == inscripcion.MateriaId &&
                        i.ProfesorId == inscripcion.ProfesorId &&
                        i.EstudianteId != estudianteId)
                    .Include(i => i.Estudiante)
                    .Select(i => i.Estudiante)
                    .Distinct()
                    .ToListAsync();

                companeros.AddRange(estudiantesMismaClase);
            }

            var unicos = companeros.DistinctBy(e => e.Id).ToList();
            return _mapper.Map<IEnumerable<EstudianteReadDTO>>(unicos);
        }
    }
}
