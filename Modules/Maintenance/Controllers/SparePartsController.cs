using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TT.Backend.Modules.Maintenance.DTOs;
using TT.Backend.Modules.Maintenance.Services;

namespace TT.Backend.Modules.Maintenance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/maintenance/spareparts")]
    public class SparePartsController : ControllerBase
    {
        private readonly ISparePartService _service;
        public SparePartsController(ISparePartService service) => _service = service;

        [HttpGet]
        public ActionResult GetAll() => Ok(_service.GetAllParts());

        [HttpGet("lowstock")]
        public ActionResult GetLowStock() => Ok(_service.GetLowStockParts());

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var part = _service.GetPartById(id);
            if (part == null) return NotFound(new { error = "Pièce introuvable" });
            return Ok(part);
        }

        [HttpGet("{id}/history")]
        public ActionResult GetHistory(Guid id) =>
            Ok(_service.GetUsageHistory(id));

        [HttpPost]
        public ActionResult Create([FromBody] CreateSparePartRequest request)
        {
            var part = _service.CreatePart(request);
            return CreatedAtAction(nameof(GetById), new { id = part.Id }, part);
        }

        [HttpPatch("{id}/restock")]
        public ActionResult Restock(Guid id, [FromBody] int quantity)
        {
            _service.RestockPart(id, quantity);
            return NoContent();
        }

        [HttpPost("use")]
        public ActionResult Use([FromBody] UseSparePartRequest request)
        {
            try
            {
                var usage = _service.UsePart(request);
                return Ok(usage);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}