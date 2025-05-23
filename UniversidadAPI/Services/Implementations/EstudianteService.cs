using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.DTOs;
using UniversidadAPI.Models.Entities;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Services.Implementations
{
    public class EstudianteService : IEstudianteService
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public EstudianteService(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EstudianteReadDTO> CreateEstudianteAsync(EstudianteCreateDTO estudianteDto)
        {
            // Validación de email único
            if (await _context.Estudiantes.AnyAsync(e => e.Email == estudianteDto.Email))
            {
                throw new InvalidOperationException("El email ya está registrado");
            }

            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return _mapper.Map<EstudianteReadDTO>(estudiante);
        }

        public async Task<IEnumerable<EstudianteReadDTO>> GetAllEstudiantesAsync()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return _mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes);
        }

        public async Task<EstudianteReadDTO> GetEstudianteByIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            return estudiante == null ? null : _mapper.Map<EstudianteReadDTO>(estudiante);
        }

        public async Task<bool> UpdateEstudianteAsync(int id, EstudianteUpdateDTO estudianteDto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return false;

            // Validación de email único (solo si cambia el email)
            if (!string.IsNullOrEmpty(estudianteDto.Email) &&
                estudianteDto.Email != estudiante.Email &&
                await _context.Estudiantes.AnyAsync(e => e.Email == estudianteDto.Email))
            {
                throw new InvalidOperationException("El nuevo email ya está registrado");
            }

            // Actualiza solo los campos 
            _mapper.Map(estudianteDto, estudiante);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteEstudianteAsync(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Inscripciones)  // Carga relacionada si es necesario
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return false;

            // Validación adicional, no permitir eliminar si tiene inscripciones
            if (estudiante.Inscripciones?.Any() == true)
            {
                throw new InvalidOperationException("No se puede eliminar el estudiante porque tiene inscripciones activas");
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
