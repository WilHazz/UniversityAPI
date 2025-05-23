using UniversidadAPI.DTOs;

namespace UniversidadAPI.Services.Interfaces
{
    public interface IEstudianteService
    {
        Task<EstudianteReadDTO> CreateEstudianteAsync(EstudianteCreateDTO estudianteDto);
        Task<IEnumerable<EstudianteReadDTO>> GetAllEstudiantesAsync();
        Task<EstudianteReadDTO> GetEstudianteByIdAsync(int id);
        Task<bool> UpdateEstudianteAsync(int id, EstudianteUpdateDTO estudianteDto);
        Task<bool> DeleteEstudianteAsync(int id);
    }

   
}
