using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinDex.Api.Data.Entities;
using VinDex.Api.Data.Repositories;

namespace VinDex.Api.Controllers
{
    [ApiController]
    [Route("api/maintenance")]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceRecordRepository _repo;
        public MaintenanceController(IMaintenanceRecordRepository maintenanceRepo)
        {
            _repo = maintenanceRepo;
        }

        [HttpGet("{vin}")]
        public async Task<IActionResult> GetByVin(string vin)
        {
            var records = await _repo.GetByVinAsync(vin);
            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MaintenanceRecordEntity record)
        {
            var created = await _repo.AddAsync(record);
            return CreatedAtAction(nameof(GetByVin), new { vin = created.VehicleVin }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MaintenanceRecordEntity record)
        {
            if (id != record.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(record);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
