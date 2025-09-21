using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosureRepository;

        public EnclosuresController(IEnclosureRepository enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enclosures = await _enclosureRepository.GetAllAsync();

            // Преобразуем доменные сущности в DTO
            var enclosureResponses = enclosures.Select(enclosure => new EnclosureResponse
            {
                Id = enclosure.Id,
                Type = enclosure.Type,
                Size = enclosure.Size,
                CurrentCapacity = enclosure.CurrentCapacity,
                MaxCapacity = enclosure.MaxCapacity,
                IsClean = enclosure.IsClean
            }).ToList();

            return Ok(enclosureResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enclosure = await _enclosureRepository.GetByIdAsync(id);
            if (enclosure == null) return NotFound();
            return Ok(enclosure);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnclosureRequest request)
        {
            var enclosure = new Enclosure(request.Type, request.Size, request.MaxCapacity);
            await _enclosureRepository.AddAsync(enclosure);
            return CreatedAtAction(nameof(GetById), new { id = enclosure.Id }, enclosure);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _enclosureRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/clean")]
        public async Task<IActionResult> CleanEnclosure(Guid id)
        {
            var enclosure = await _enclosureRepository.GetByIdAsync(id);
            if (enclosure == null) return NotFound();

            enclosure.Clean();
            await _enclosureRepository.UpdateAsync(enclosure);
            return Ok();
        }
    }
}
