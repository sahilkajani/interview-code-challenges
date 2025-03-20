using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess.Fine;
using OneBeyondApi.DataAccess.OnLoan;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnLoanController : ControllerBase
    {
        private readonly IOnLoanRepository _onLoanRepository;
        private readonly IFineCalculator _fineCalculator;
        private readonly IFineRepository _fineRepository;

        public OnLoanController(IOnLoanRepository onLoanRepository, IFineCalculator fineCalculator, IFineRepository fineRepository)
        {
            _onLoanRepository = onLoanRepository;
            _fineCalculator = fineCalculator;
            _fineRepository = fineRepository;
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

        [HttpPost]
        [Route("ReturnOnLoanBooks")]
        public async Task<IActionResult> ReturnBooksOnLoan(Guid bookId)
        {
            var bookStock = await _onLoanRepository.GetBookOnLoanByIdAsync(bookId);

            if (bookStock == null)
            {
                return NotFound($"No book found that is on loan for Book Id: {bookId}");
            }

            if (bookStock.LoanEndDate < DateTime.Today)
            {
                var fineAmount = _fineCalculator.Calculate(bookStock);

                var fine = new Fine()
                {
                    BookLoaned = bookStock.Book.Name,
                    Amount = fineAmount,
                    FineDate = DateTime.Today,
                };

                await _fineRepository.AddFineAsync(fine);
            }

            bookStock.OnLoanTo = null;
            bookStock.LoanEndDate = null;

            _onLoanRepository.UpdateBookAsReturnedAsync(bookStock);

            return Ok();
        }
    }
}
