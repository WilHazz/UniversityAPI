using UniversidadAPI.DTOs;

namespace UniversidadAPI.Services.Interfaces
{
    public interface IProfesorService
    {
        Task<ProfesorReadDTO> CreateProfesorAsync(ProfesorCreateDTO profesorDto);
        Task<IEnumerable<ProfesorReadDTO>> GetAllProfesoresAsync();

        Task<ProfesorReadDTO> GetProfesorByIdAsync(int id);

        Task<bool> UpdateProfesorAsync(int id, ProfesorUpdateDTO profesorDTO);
        Task<bool> DeleteProfesorAsync(int id);
    }
}
