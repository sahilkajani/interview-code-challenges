using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess.Reservations;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;    
        }

        [HttpPost]
        [Route("ReserveBook")]
        public async Task<IActionResult> ReserveBook(Guid bookId, Guid borrowerId)
        {
            try
            {
                var bookStock = await _reservationRepository.GetBookOnLoanByIdAsync(bookId);

                if (bookStock == null)
                {
                    return NotFound($"No book found that is on loan for Book Id: {bookId}");
                }

                var borrower = await _reservationRepository.GetBorrowerByIdAsync(borrowerId);

                if (borrower == null)
                {
                    return NotFound($"No borrower found with Borrower Id: {borrowerId}");
                }

                if (borrower.Id == bookStock.OnLoanTo.Id)
                {
                    return BadRequest(
                    $"Unable to reserve book for Borrower Id: {borrowerId} as book is already on loan to borrower");
                }

                if (IsNewReservation(bookStock))
                {
                    var reservedBook = await AddNewReservationAsync(bookStock, borrower);

                    return Ok($"Book reserved from {reservedBook.Reservations?.First().ReservedFrom} to {reservedBook.Reservations?.First().ReservedTo}");
                }

                if (IsBorrowerAlreadyInReservationList(bookStock, borrowerId))
                {
                    return BadRequest($"Borrower Id: {borrowerId} has already reserved the book");
                }

                var book = await AddToExistingReservationListAsync(bookStock, borrower);

                return Ok($"Book reserved from {book.Reservations?.Last().ReservedFrom} to {book.Reservations?.Last().ReservedTo}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<BookStock> AddToExistingReservationListAsync(BookStock? bookStock, Borrower? borrower)
        {
            var bookReservation = new Reservation()
            {
                Id = Guid.NewGuid(),
                Borrower = borrower,
                BorrowerId = borrower.Id,
                ReservedFrom = bookStock.Reservations.Last().ReservedTo.AddDays(1),
                ReservedTo = bookStock.Reservations.Last().ReservedTo.AddDays(15)
            };

            bookStock.Reservations.Add(bookReservation);

            await _reservationRepository.AddBorrowerInReservationListAsync(bookStock, bookReservation);

            return bookStock;
        }

        private static bool IsNewReservation(BookStock bookStock)
        {
            return bookStock.Reservations == null || bookStock.Reservations.Count == 0;
        }

        private async Task<BookStock> AddNewReservationAsync(BookStock? bookStock, Borrower? borrower)
        {
            var reservation = new Reservation()
            {
                Id = Guid.NewGuid(),
                Borrower = borrower,
                BorrowerId = borrower.Id,
                ReservedFrom = bookStock.LoanEndDate.Value.AddDays(1),
                ReservedTo = bookStock.LoanEndDate.Value.AddDays(15)
            };

            bookStock.Reservations = new List<Reservation>() { reservation };

            await _reservationRepository.AddBorrowerInReservationListAsync(bookStock, reservation);

            return bookStock;
        }

        private bool IsBorrowerAlreadyInReservationList(BookStock bookStock, Guid borrowerId)
        {
            var borrower = bookStock.Reservations.Where(x => x.BorrowerId == borrowerId);

            return (borrower.Any());
        }
    }
}
