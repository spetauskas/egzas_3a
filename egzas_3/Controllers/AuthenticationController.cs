using Microsoft.AspNetCore.Mvc;
using egzas_3.Services;
using egzas_3.Services.Interfaces;
using egzas_3.Dtos.Requests;
using System.Net.Mime;
using egzas_3.Repositories.Interfaces;

namespace egzas_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;
        private readonly IAccountRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly IAccountService _service;

        public AuthController(IAccountService service, IJwtService jwtService, IAccountRepository repository, ILogger<AuthController> logger, IAuthService authService)
        {
            //_service = authService;
            _logger = logger;
            _repository = repository;
            _jwtService = jwtService;
            _service = service;
        }

        // Login DTO
        public class LoginDto
        {
            public string AccountName { get; set; } = string.Empty;
            public string AccountPassword { get; set; } = string.Empty;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        //{
        //    try
        //    {
        //        var token = await _authService.Login(loginDto.AccountName, loginDto.AccountPassword);
        //        return Ok(new { Token = token });
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Unauthorized(new { Message = "Invalid username or password" });
        //    }
        //}
        [HttpPost("Login")]
        [Produces(MediaTypeNames.Text.Plain)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login(LoginRequestDto req)
        {
            _logger.LogInformation($"Login attempt for {req.AccountName}");
            var account = _repository.Get(req.AccountName!);
            if (account == null)
            {
                _logger.LogWarning($"User {req.AccountName} not found");
                return BadRequest("User nor found");
            }

            var isPasswordValid = _service.VerifyPasswordHash(req.AccountPassword, account.AccountPasswordHash, account.AccountPasswordSalt);

            if (!isPasswordValid)
            {
                _logger.LogWarning($"Invalid password for {req.AccountName}");
                return BadRequest("Invalid username or password");
            }
            _logger.LogInformation($"User {req.AccountName} successfully logged in");
            var jwt = _jwtService.GetJwtToken(account);
            return Ok(jwt);

        }
    }
}