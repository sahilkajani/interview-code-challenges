using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess.OnLoan;

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
        public ActionResult GetCurrentBooksOnLoan()
        {
            var booksOnLoan = _onLoanRepository.GetBooksOnLoan();

            if (booksOnLoan == null || !booksOnLoan.Any())
            {
                return NotFound("Unable to find any books on loan currently");
            }

            return Ok(booksOnLoan);
        }
    }
}
