using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Models;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Example()
        {
            var sample = new
            {
                firstName = "Jane",
                lastName = "Doe",
                email = "jane.doe@example.com",
                password = "Password1",
                confirmPassword = "Password1",
                age = 30
            };

            return Ok(sample);
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // Here you would typically create the user, hash the password, and store in DB, etc.
            _logger.LogInformation("New registration: {Email} - {FirstName} {LastName}", request.Email, request.FirstName, request.LastName);

            var response = new
            {
                message = "Registration successful",
                email = request.Email,
                firstName = request.FirstName,
                lastName = request.LastName,
                age = request.Age
            };

            return CreatedAtAction(nameof(Register), response);
        }
    }
}
    