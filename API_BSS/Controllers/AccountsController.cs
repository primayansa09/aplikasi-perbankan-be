using API_BSS.Controllers.DTO;
using API_BSS.Data;
using API_BSS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API_BSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly APIDBContext dbContext;

        public AccountsController(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRekening([FromForm]AddAccountDTO addAccount) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(new ApiResponseDTO<object>
                {
                    Status = 400,
                    Message = "Validation failed",
                    Data = ModelState
                });
            }
            try
            {
                var dateNow = DateTime.Now;

                var passwordHasher = new PasswordHasher<Users>();

                var userId = Guid.NewGuid();
                var accountId = Guid.NewGuid();
                var accountNumber = await GenerateAccountNumber();

                var passwordHash = passwordHasher.HashPassword(
                    new Users(),
                    addAccount.Password
                );

                var list = await dbContext.Database
                    .SqlQueryRaw<AddAccountResultDTO>(
                        "EXEC sp_AddAccount @UserId, @FullName, @Email, @Password, @AccountId, @Account, @Amount, @CreatedAt",
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@FullName", addAccount.FullName),
                        new SqlParameter("@Email", addAccount.Email),
                        new SqlParameter("@Password", passwordHash),
                        new SqlParameter("@AccountId", accountId),
                        new SqlParameter("@Account", accountNumber),
                        new SqlParameter("@Amount", 10000000),
                        new SqlParameter("@CreatedAt", dateNow)
                    )
                    .ToListAsync();

                var result = list.FirstOrDefault();

                if (result.Status != 200)
                {
                    return BadRequest(new ApiResponseDTO<object>
                    {
                        Status = result.Status,
                        Message = result.Message
                    });

                }

                return Ok(new ApiResponseDTO<object>
                {
                    Status = result.Status,
                    Message =result.Message,
                    Data = new
                    {
                        result.UserId,
                        result.FullName,
                        result.Email,
                        result.Account,
                        result.Amount
                    }
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ApiResponseDTO<object>
                {
                    Status = 500,
                    Message = "Terjadi kesalahan pada server",
                    Data = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDTO<object>
                {
                    Status = 400,
                    Message = "Validation failed",
                    Data = ModelState
                });
            }

            try
            {
                var result = await dbContext.Database
                    .SqlQueryRaw<AccountBalanceDTO>(
                        "EXEC sp_GetAccountBalance @Id",
                        new SqlParameter("@Id", id)
                    )
                    .ToListAsync();

                var account = result.FirstOrDefault();

                if (account.Id == null)
                    return BadRequest();

                return Ok(new ApiResponseDTO<object>
                {
                    Status = 200,
                    Message = "Success",
                    Data = new
                    {
                        account.AccountId,
                        account.Id,
                        account.Account,
                        account.Amount,
                        account.FullName,
                        account.Email,
                        account.CreatedAt,
                        account.DeletedAt,
                        account.IsDeleted
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDTO<object>
                {
                    Status = 500,
                    Message = "Internal Server Error",
                    Data = ex.Message
                });
            }
        }

        private async Task<string> GenerateAccountNumber()
        {
            var today = DateTime.UtcNow.ToString("yyMMdd");

            var lastAccount = await dbContext.Accounts
                .Where(a => a.Account.StartsWith(today))
                .OrderByDescending(a => a.Account)
                .Select(a => a.Account)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastAccount))
            {
                var lastSequence = int.Parse(lastAccount.Substring(6));
                nextNumber = lastSequence + 1;
            }

            return $"{today}{nextNumber:D6}";
        }
    }
}
