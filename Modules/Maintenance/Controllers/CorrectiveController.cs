using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.DTOs;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/maintenance/corrective")]
    public class CorrectiveController : ControllerBase
    {
        private readonly ICorrectiveService _service;
        public CorrectiveController(ICorrectiveService service) => _service = service;

        [HttpGet]
        public ActionResult GetAll() => Ok(_service.GetAllTasks());

        [HttpGet("equipment/{equipmentId}")]
        public ActionResult GetByEquipment(Guid equipmentId) =>
            Ok(_service.GetTasksByEquipment(equipmentId));

        [HttpGet("status/{status}")]
        public ActionResult GetByStatus(CorrectiveStatus status) =>
            Ok(_service.GetByStatus(status));

        [HttpPost]
        public ActionResult Report([FromBody] CreateCorrectiveTaskRequest request)
        {
            var task = _service.ReportIssue(request);
            return Ok(task);
        }

        [HttpPatch("{id}/status")]
        public ActionResult UpdateStatus(Guid id, [FromBody] CorrectiveStatus status)
        {
            _service.UpdateStatus(id, status);
            return NoContent();
        }

        [HttpPatch("{id}/resolve")]
        public ActionResult Resolve(Guid id, [FromBody] string solution)
        {
            _service.ResolveTask(id, solution);
            return NoContent();
        }
    }
}