using FlightManagement.AlertApi.DTOs;
using FlightManagement.AlertApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.AlertApi.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertServicetion;

        public AlertsController(IAlertService service)
        {
            _alertServicetion = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlertDto dto)
        {
            await _alertServicetion.CreateAsync(dto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _alertServicetion.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var result = await _alertServicetion.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlertDto dto)
        {
            await _alertServicetion.UpdateAsync(id, dto);
            return Ok(new { message = "Alert updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _alertServicetion.DeleteAsync(id);
            return Ok(new { message = "Alert deleted successfully." });
        }
    }
}
