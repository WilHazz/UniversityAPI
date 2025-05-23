using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.DTOs;
using UniversidadAPI.Models.Entities;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Services.Implementations
{
    public class ProfesorMateriaService : IProfesorMateriaService
    {
        private readonly UniversidadContext _context;

        public ProfesorMateriaService(UniversidadContext context)
        {
            _context = context;
        }

        public async Task<ProfesorMateriaReadDTO> CreateAsync(int profesorId, int materiaId)
        {
            var exists = await _context.ProfesorMaterias
                .AnyAsync(pm => pm.ProfesorId == profesorId && pm.MateriaId == materiaId);

            if (exists)
                throw new InvalidOperationException("Ya existe esta relación Profesor-Materia.");

            var pm = new ProfesorMateria
            {
                ProfesorId = profesorId,
                MateriaId = materiaId
            };

            _context.ProfesorMaterias.Add(pm);
            await _context.SaveChangesAsync();

            // Traer con nombres
            var result = await _context.ProfesorMaterias
                .Include(p => p.Profesor)
                .Include(p => p.Materia)
                .FirstOrDefaultAsync(p => p.Id == pm.Id);

            return new ProfesorMateriaReadDTO
            {
                Id = result.Id,
                ProfesorId = result.ProfesorId,
                ProfesorNombre = result.Profesor.Nombre,
                MateriaId = result.MateriaId,
                MateriaNombre = result.Materia.Nombre
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pm = await _context.ProfesorMaterias.FindAsync(id);
            if (pm == null) return false;

            _context.ProfesorMaterias.Remove(pm);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProfesorMateriaReadDTO>> GetAllAsync()
        {
            return await _context.ProfesorMaterias
                .Include(pm => pm.Profesor)
                .Include(pm => pm.Materia)
                .Select(pm => new ProfesorMateriaReadDTO
                {
                    Id = pm.Id,
                    ProfesorId = pm.ProfesorId,
                    ProfesorNombre = pm.Profesor.Nombre,
                    MateriaId = pm.MateriaId,
                    MateriaNombre = pm.Materia.Nombre
                })
                .ToListAsync();
        }

        public async Task<ProfesorMateriaReadDTO?> GetByIdAsync(int id)
        {
            var pm = await _context.ProfesorMaterias
                .Include(p => p.Profesor)
                .Include(p => p.Materia)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pm == null) return null;

            return new ProfesorMateriaReadDTO
            {
                Id = pm.Id,
                ProfesorId = pm.ProfesorId,
                ProfesorNombre = pm.Profesor.Nombre,
                MateriaId = pm.MateriaId,
                MateriaNombre = pm.Materia.Nombre
            };
        }
    }
}
