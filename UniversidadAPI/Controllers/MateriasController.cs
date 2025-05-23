using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.DTOs;
using UniversidadAPI.Services.Interfaces;


namespace UniversidadAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriasController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaReadDTO>>> GetAll()
        {
            var materias = await _materiaService.GetAllMateriasAsync();
            return Ok(materias);
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaReadDTO>> GetById(int id)
        {
            var materia = await _materiaService.GetMateriaByIdAsync(id);
            return materia == null ? NotFound() : Ok(materia);
        }

        // POST: api/Materias
        [HttpPost]
        public async Task<ActionResult<MateriaReadDTO>> Create(MateriaCreateDTO dto)
        {
            var nuevaMateria = await _materiaService.CreateMateriaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaMateria.Id }, nuevaMateria);
        }

        // PUT: api/Materias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MateriaUpdateDTO dto)
        {
            var result = await _materiaService.UpdateMateriaAsync(id, dto);
            return result ? NoContent() : NotFound();
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _materiaService.DeleteMateriaAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
