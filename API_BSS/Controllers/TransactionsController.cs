using API_BSS.Controllers.DTO;
using API_BSS.Data;
using API_BSS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API_BSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly APIDBContext dbContext;
        
        public TransactionsController(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Transaction(RequestTransactionDTO requestTransaction)
        {
            try
            {
                var result = dbContext.Database
                    .SqlQueryRaw<TransferResultDTO>(
                        "EXEC sp_TransferFund @SourceAccount, @DestinationAccount, @Amount, @Description",
                        new SqlParameter("@SourceAccount", requestTransaction.SourceAccount),
                        new SqlParameter("@DestinationAccount", requestTransaction.DestinationAccount),
                        new SqlParameter("@Amount", requestTransaction.Amount),
                        new SqlParameter("@Description", requestTransaction.Description ?? (object)DBNull.Value)
                    )
                    .AsEnumerable()
                    .FirstOrDefault();

                if (result == null)
                    return BadRequest("Tranfer gagal");

                return Ok(new ApiResponseDTO<object>
                {
                    Status = result.Status,
                    Message = result.Message,
                    Data = new
                    {
                        result.SourceAccount,
                        result.DestinationAccount,
                        result.Amount,
                        result.CreatedAt,
                        result.Description
                    }
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
                
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTransaction(Guid id)
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
            var transactions = await dbContext.Database
                .SqlQueryRaw<TransactionResultDTO>(
                    "EXEC sp_GetLast5Transactions @AccountId",
                    new SqlParameter("@AccountId", id)
                )
                .ToListAsync();

                if (transactions == null)
                return BadRequest();

                return Ok(new ApiResponseDTO<object>
                {
                    Status = 200,
                    Message = "Success",
                    Data = transactions
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
    }
}
