using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Implementations;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionService _service;

        public InscripcionesController(IInscripcionService service)
        {
            _service = service;
        }

        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionReadDTO>>> GetAll()
        {
            var inscripciones = await _service.GetAllAsync();
            return Ok(inscripciones);
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionReadDTO>> GetById(int id)
        {
            var inscripcion = await _service.GetByIdAsync(id);
            if (inscripcion == null)
                return NotFound();

            return Ok(inscripcion);
        }

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<InscripcionReadDTO>> Create(InscripcionCreateDTO dto)
        {
            try
            {
                var nuevaInscripcion = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaInscripcion.Id }, nuevaInscripcion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        //GET /api/Inscripciones/companeros/5
        [HttpGet("companeros/{estudianteId}")]
        public async Task<ActionResult<IEnumerable<EstudianteReadDTO>>> GetCompanerosDeClase(int estudianteId)
        {
            try
            {
                var companeros = await _service.GetCompanerosDeClaseAsync(estudianteId);
                return Ok(companeros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener compañeros de clase: {ex.Message}");
            }
        }

    }
}
