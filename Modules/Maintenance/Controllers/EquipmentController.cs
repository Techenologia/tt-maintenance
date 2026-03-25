using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.DTOs;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/maintenance/equipment")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var equipments = await _service.GetAllEquipments();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var equipment = await _service.GetEquipmentById(id);
            if (equipment == null)
                return NotFound(new { error = "Équipement introuvable", id });
            return Ok(equipment);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEquipmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var equipment = await _service.CreateEquipment(request);
            return CreatedAtAction(nameof(GetById), new { id = equipment.Id }, equipment);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] EquipmentStatus status)
        {
            var exists = await _service.GetEquipmentById(id);
            if (exists == null)
                return NotFound(new { error = "Équipement introuvable", id });

            await _service.UpdateStatus(id, status);
            return NoContent();
        }
    }
}