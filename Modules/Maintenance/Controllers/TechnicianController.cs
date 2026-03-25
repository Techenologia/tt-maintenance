using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.DTOs;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/maintenance/technicians")]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianService _service;
        public TechnicianController(ITechnicianService service) => _service = service;

        [HttpGet]
        public ActionResult GetAll() => Ok(_service.GetAllTechnicians());

        [HttpGet("available")]
        public ActionResult GetAvailable() => Ok(_service.GetAvailableTechnicians());

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var technician = _service.GetTechnicianById(id);
            if (technician == null) return NotFound(new { error = "Technicien introuvable" });
            return Ok(technician);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateTechnicianRequest request)
        {
            var technician = _service.CreateTechnician(request);
            return CreatedAtAction(nameof(GetById), new { id = technician.Id }, technician);
        }

        [HttpPatch("{id}/status")]
        public ActionResult UpdateStatus(Guid id, [FromBody] TechnicianStatus status)
        {
            _service.UpdateStatus(id, status);
            return NoContent();
        }

        [HttpPatch("{technicianId}/assign/preventive/{taskId}")]
        public ActionResult AssignToPreventive(Guid technicianId, Guid taskId)
        {
            _service.AssignToPreventiveTask(technicianId, taskId);
            return NoContent();
        }

        [HttpPatch("{technicianId}/assign/corrective/{taskId}")]
        public ActionResult AssignToCorrective(Guid technicianId, Guid taskId)
        {
            _service.AssignToCorrectiveTask(technicianId, taskId);
            return NoContent();
        }
    }
}