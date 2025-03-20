using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Reservations
{
    public interface IReservationRepository
    {
        Task<BookStock?> GetBookOnLoanByIdAsync(Guid bookId);
        Task<Borrower?> GetBorrowerByIdAsync(Guid borrowerId);
        Task AddBorrowerInReservationListAsync(BookStock bookStock, Reservation reservation);
    }
}
