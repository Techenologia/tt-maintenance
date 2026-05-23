// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.DTOs;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/maintenance/preventive")]
    public class PreventiveController : ControllerBase
    {
        private readonly IPreventiveService _service;
        public PreventiveController(IPreventiveService service) => _service = service;

        [HttpGet]
        public ActionResult GetAll() => Ok(_service.GetAllTasks());

        [HttpGet("equipment/{equipmentId}")]
        public ActionResult GetByEquipment(Guid equipmentId) =>
            Ok(_service.GetTasksByEquipment(equipmentId));

        [HttpGet("overdue")]
        public ActionResult GetOverdue() => Ok(_service.GetOverdueTasks());

        [HttpPost]
        public ActionResult Create([FromBody] CreatePreventiveTaskRequest request)
        {
            var task = _service.CreateTask(request);
            return Ok(task);
        }

        [HttpPatch("{id}/complete")]
        public ActionResult Complete(Guid id)
        {
            _service.CompleteTask(id);
            return NoContent();
        }
    }
}

