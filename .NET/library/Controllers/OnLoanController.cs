using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess.OnLoan;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    public class OnLoanController : ControllerBase
    {
        private readonly IOnLoanRepository _onLoanRepository;

        public OnLoanController(IOnLoanRepository onLoanRepository)
        {
            _onLoanRepository = onLoanRepository;    
        }

        [HttpGet]
        [Route("GetCurrentBooksOnLoan")]
        public async Task<ActionResult<OnLoan>> GetCurrentBooksOnLoan()
        {
            var booksOnLoan = await _onLoanRepository.GetBooksOnLoanAsync();

            if (booksOnLoan == null || !booksOnLoan.Any())
            {
                return NotFound("Unable to find any books on loan currently");
            }

            return Ok(booksOnLoan);
        }
    }
}
