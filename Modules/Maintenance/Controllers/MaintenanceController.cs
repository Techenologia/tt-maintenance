// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.Services;
using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/maintenance-tasks")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _service;

        public MaintenanceController(IMaintenanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _service.GetTaskById(id);
            if (task == null)
                return NotFound(new { error = "TÃ¢che introuvable", id });
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaintenanceTaskEntity task)
        {
            if (task == null)
                return BadRequest(new { error = "DonnÃ©es invalides" });
            await _service.CreateTask(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
    }
}

