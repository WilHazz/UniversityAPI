using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Interfaces;

namespace UniversidadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        // GET: api/Profesores - Lista todos los profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorReadDTO>>> GetAll()
        {
            var profesores = await _profesorService.GetAllProfesoresAsync();
            return Ok(profesores);
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorReadDTO>> GetById(int id)
        {
            var profesor = await _profesorService.GetProfesorByIdAsync(id);
            return profesor == null ? NotFound() : Ok(profesor);
        }

        // POST: api/Profesores
        [HttpPost]
        public async Task<ActionResult<ProfesorReadDTO>> Create(ProfesorCreateDTO dto)
        {
            try
            {
                var nuevoProfesor = await _profesorService.CreateProfesorAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoProfesor.Id }, nuevoProfesor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Profesores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProfesorUpdateDTO dto)
        {
            try
            {
                var result = await _profesorService.UpdateProfesorAsync(id, dto);
                return result ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _profesorService.DeleteProfesorAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
