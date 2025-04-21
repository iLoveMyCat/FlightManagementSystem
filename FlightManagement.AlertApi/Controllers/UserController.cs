using FlightManagement.AlertApi.DTOs;
using FlightManagement.AlertApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.AlertApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var id = Guid.NewGuid();
            await _userService.EnsureExistsAsync(id, dto.Email);
            return Ok(new { Id = id});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("{id}/device-token")]
        public async Task<IActionResult> RegisterDeviceToken(Guid id, [FromBody] RegisterDeviceTokenDto dto)
        {
            await _userService.RegisterDeviceTokenAsync(id, dto.DeviceToken);
            return Ok(new { message = "Device token registered successfully." });
        }
    }
}
