using API_BSS.Controllers.DTO;
using API_BSS.Data;
using API_BSS.Model;
using API_BSS.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers.DTO;

namespace API_BSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly APIDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(APIDBContext context, IConfiguration configuration, TokenService tokenService, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, IPasswordHasher<Users> hasher)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == loginDTO.Email.ToLower());

                if (user == null)
                    return BadRequest(new { status = 401, message = "Email or Password is incorrect" });

                var result = hasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);

                if (result == PasswordVerificationResult.Failed)
                    return BadRequest(new { message = "Invalid email or paswword" });

                var account = await _context.Accounts.FirstOrDefaultAsync(c => c.UserId == user.Id);

                var accessToken = _tokenService.GenerateAccessToken(user);

                return Ok(new ApiResponseDTO<object>
                {
                    Status = 200,
                    Message = "login success",
                    Data = new
                    {
                        user = new
                        {
                            Id = user.Id,
                            FullName = user?.FullName,
                            Email = user?.Email,
                            Account = account?.Account,
                            Amount = account?.Amount,
                            AccountId = account?.Id,
                            isDeleted = account?.IsDeleted
                        },
                        token = accessToken,
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                return StatusCode(500, new ApiResponseDTO<object>
                {
                    Status = 500,
                    Message = "Internal Server Error",
                    Data = ex.Message
                });
            }

        }
    }
}
