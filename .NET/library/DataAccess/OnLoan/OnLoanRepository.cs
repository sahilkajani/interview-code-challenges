using Microsoft.EntityFrameworkCore;

namespace OneBeyondApi.DataAccess.OnLoan
{
    public class OnLoanRepository : IOnLoanRepository
    {
        private readonly LibraryContext _context;

        public OnLoanRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<OneBeyondApi.Model.OnLoan> GetBooksOnLoan()
        {
            return _context.Catalogue
                    .Include(x => x.Book)
                    .Include(x => x.OnLoanTo)
                    .Where(x => x.OnLoanTo != null)
                    .Select(x => new OneBeyondApi.Model.OnLoan()
                    {
                        BookName = x.Book.Name,
                        BorrowerEmailAddress = x.OnLoanTo.EmailAddress,
                        BorrowerName = x.OnLoanTo.Name

                    }).ToList();
        }
    }
}
