using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.OnLoan
{
    public class OnLoanRepository : IOnLoanRepository
    {
        private readonly LibraryContext _context;

        public OnLoanRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OneBeyondApi.Model.OnLoan>> GetBooksOnLoanAsync()
        {
            return await _context.Catalogue
                        .Include(x => x.Book)
                        .Include(x => x.OnLoanTo)
                        .Where(x => x.OnLoanTo != null)
                        .Select(x => new OneBeyondApi.Model.OnLoan()
                        {
                            BookName = x.Book.Name,
                            BorrowerEmailAddress = x.OnLoanTo.EmailAddress,
                            BorrowerName = x.OnLoanTo.Name

                        }).ToListAsync();
        }

        public async Task<BookStock?> GetBookOnLoanByIdAsync(Guid bookId)
        {
            return await _context.Catalogue
                .Include(x => x.Book)
                .Include(x => x.OnLoanTo)
                .Where(x => x.OnLoanTo != null)
                .FirstOrDefaultAsync(x => x.Book.Id == bookId);
        }

        public void UpdateBookAsReturnedAsync(BookStock bookStock)
        {
            _context.Catalogue.Update(bookStock);
            _context.SaveChanges();
        }
    }
}
