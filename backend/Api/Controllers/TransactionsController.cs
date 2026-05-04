using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactiosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTransactions()
        {
            return Ok(new List<string> { "Transaction 1", "Transaction 2", "Transaction 3" });
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] string transaction)
        {
            // Implementation for creating a new transaction
            return CreatedAtAction(nameof(GetTransactions), new { id = transaction }, transaction);
        }
    }
}