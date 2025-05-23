using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Controllers
{
    public class ProfesorMateriaController : ControllerBase
    {
        private readonly IProfesorMateriaService _service;

        public ProfesorMateriaController(IProfesorMateriaService service)
        {
            _service = service;
        }

        // GET: api/ProfesorMateria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorMateriaReadDTO>>> GetAll()
        {
            var relaciones = await _service.GetAllAsync();
            return Ok(relaciones);
        }

        // GET: api/ProfesorMateria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorMateriaReadDTO>> GetById(int id)
        {
            var relacion = await _service.GetByIdAsync(id);
            if (relacion == null)
                return NotFound();
            return Ok(relacion);
        }

        // POST: api/ProfesorMateria
        [HttpPost]
        public async Task<ActionResult<ProfesorMateriaReadDTO>> Create([FromBody] CreateRelacionDTO dto)
        {
            try
            {
                var nuevaRelacion = await _service.CreateAsync(dto.ProfesorId, dto.MateriaId);
                return CreatedAtAction(nameof(GetById), new { id = nuevaRelacion.Id }, nuevaRelacion);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/ProfesorMateria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado)
                return NotFound();
            return NoContent();
        }
    }

    public class CreateRelacionDTO
    {
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
    }
}
