using JekirdekProject.BusinessLayer.Services;

using Microsoft.AspNetCore.Mvc;

namespace JekirdekProject.UILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersApiController> _logger;

        public CustomersApiController(ICustomerService customerService, ILogger<CustomersApiController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredCustomers(
            [FromQuery] string? name,
            [FromQuery] string? email,
            [FromQuery] DateTime? registrationDate,
            [FromQuery] string? region)
        {
            try
            {
                var customers = await _customerService.GetFilteredCustomers(name, email, registrationDate, region);

                if (customers == null || !customers.Any())
                {
                    return NotFound("No customers found matching the given filters.");
                }

                return Ok(customers);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Argument error in GetFilteredCustomers: {ex.Message}");
                return BadRequest("Invalid input parameters.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetFilteredCustomers: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
