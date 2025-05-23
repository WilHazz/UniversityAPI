using UniversidadAPI.DTOs;

namespace UniversidadAPI.Services.Interfaces
{
    public interface IMateriaService
    {
        Task<IEnumerable<MateriaReadDTO>> GetAllMateriasAsync();
        Task<MateriaReadDTO?> GetMateriaByIdAsync(int id);
        Task<MateriaReadDTO> CreateMateriaAsync(MateriaCreateDTO materiaDto);
        Task<bool> UpdateMateriaAsync(int id, MateriaUpdateDTO materiaDto);
        Task<bool> DeleteMateriaAsync(int id);
    }
}
