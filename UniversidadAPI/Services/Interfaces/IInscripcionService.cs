using UniversidadAPI.DTOs;

namespace UniversidadAPI.Services.Interfaces
{
    public interface IInscripcionService
    {
        Task<IEnumerable<InscripcionReadDTO>> GetAllAsync();
        Task<InscripcionReadDTO?> GetByIdAsync(int id);
        Task<InscripcionReadDTO> CreateAsync(InscripcionCreateDTO inscripcionDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EstudianteReadDTO>> GetCompanerosDeClaseAsync(int estudianteId);
    }
}
