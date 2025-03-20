using Microsoft.EntityFrameworkCore;

namespace OneBeyondApi.DataAccess.OnLoan
{
    public class OnLoanRepository : IOnLoanRepository
    {
        public IEnumerable<OneBeyondApi.Model.OnLoan> GetBooksOnLoan()
        {
            using (var context = new LibraryContext())
            {
                  return context.Catalogue
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
}
