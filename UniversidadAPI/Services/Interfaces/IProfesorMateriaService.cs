using UniversidadAPI.DTOs;

namespace UniversidadAPI.Services.Interfaces
{
    public interface IProfesorMateriaService
    {
        Task<IEnumerable<ProfesorMateriaReadDTO>> GetAllAsync();
        Task<ProfesorMateriaReadDTO?> GetByIdAsync(int id);
        Task<ProfesorMateriaReadDTO> CreateAsync(int profesorId, int materiaId);
        Task<bool> DeleteAsync(int id); 
    }
}
