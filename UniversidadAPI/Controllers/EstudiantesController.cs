using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Implementations;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }
        // Get: api/Estudiantes - Lista todos los estudiantes 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteReadDTO>>> GetAll()
        {
            var estudiantes = await _estudianteService.GetAllEstudiantesAsync();
            return Ok(estudiantes);
        }
        // Get: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteReadDTO>> GetById(int id)
        {
            var estudiante = await _estudianteService.GetEstudianteByIdAsync(id);
            return estudiante == null ? NotFound() : Ok(estudiante);
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteReadDTO>> Create(EstudianteCreateDTO dto)
        {
            try
            {
                var nuevoEstudiante = await _estudianteService.CreateEstudianteAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoEstudiante.Id }, nuevoEstudiante);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstudianteUpdateDTO dto)
        {
            try
            {
                var result = await _estudianteService.UpdateEstudianteAsync(id, dto);
                return result ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _estudianteService.DeleteEstudianteAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
