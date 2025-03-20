using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.OnLoan
{
    public interface IOnLoanRepository
    {
        Task<IEnumerable<OneBeyondApi.Model.OnLoan>> GetBooksOnLoanAsync();

        Task<BookStock?> GetBookOnLoanByIdAsync(Guid bookId);

        void UpdateBookAsReturnedAsync(BookStock bookStock);
    }
}
