using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.DTOs;
using UniversidadAPI.Models.Entities;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Services.Implementations
{
    public class MateriaService : IMateriaService
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public MateriaService(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MateriaReadDTO> CreateMateriaAsync(MateriaCreateDTO materiaDto)
        {
            var materia = _mapper.Map<Materia>(materiaDto);
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return _mapper.Map<MateriaReadDTO>(materia);
        }

        public async Task<bool> DeleteMateriaAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return false;

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MateriaReadDTO>> GetAllMateriasAsync()
        {
            var materias = await _context.Materias.ToListAsync();
            return _mapper.Map<IEnumerable<MateriaReadDTO>>(materias);
        }

        public async Task<MateriaReadDTO?> GetMateriaByIdAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            return materia == null ? null : _mapper.Map<MateriaReadDTO>(materia);
        }

        public async Task<bool> UpdateMateriaAsync(int id, MateriaUpdateDTO materiaDto)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return false;

            _mapper.Map(materiaDto, materia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
