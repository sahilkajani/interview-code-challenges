using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Reservations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryContext _context;

        public ReservationRepository(LibraryContext context)
        {
            _context = context;     
        }
        public async Task<BookStock?> GetBookOnLoanByIdAsync(Guid bookId)
        {
            return await _context.Catalogue
                .Include(x => x.Book)
                .Include(x => x.OnLoanTo)
                .Include(x => x.Reservations)
                .Where(x => x.OnLoanTo != null)
                .FirstOrDefaultAsync(x => x.Book.Id == bookId);
        }

        public async Task<Borrower?> GetBorrowerByIdAsync(Guid borrowerId)
        {
            return await _context.Borrowers
                         .FirstOrDefaultAsync(x => x.Id == borrowerId);
        }

        public async Task AddBorrowerInReservationListAsync(BookStock bookStock, Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.Catalogue.Update(bookStock);
            await _context.SaveChangesAsync();
        }
    }
}
