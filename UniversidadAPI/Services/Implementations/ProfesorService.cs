using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Interfaces;
using UniversidadAPI.Data;
using UniversidadAPI.Models.Entities;

namespace UniversidadAPI.Services.Implementations
{
    public class ProfesorService : IProfesorService

    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public ProfesorService(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProfesorReadDTO> CreateProfesorAsync(ProfesorCreateDTO profesorDto)
        {
            var profesor = _mapper.Map<Profesor>(profesorDto);
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProfesorReadDTO>(profesor);
        }

        public async Task<bool> DeleteProfesorAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null) return false;

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProfesorReadDTO>> GetAllProfesoresAsync()
        {
            var profesores = await _context.Profesores.ToListAsync();
            return _mapper.Map<IEnumerable<ProfesorReadDTO>>(profesores);
        }

        public async Task<ProfesorReadDTO> GetProfesorByIdAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            return profesor == null ? null : _mapper.Map<ProfesorReadDTO>(profesor);
        }

        public async Task<bool> UpdateProfesorAsync(int id, ProfesorUpdateDTO profesorDTO)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null) return false;

            _mapper.Map(profesorDTO, profesor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
