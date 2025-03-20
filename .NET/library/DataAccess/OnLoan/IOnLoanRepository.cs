namespace OneBeyondApi.DataAccess.OnLoan
{
    public interface IOnLoanRepository
    {
        Task<IEnumerable<OneBeyondApi.Model.OnLoan>> GetBooksOnLoanAsync();
    }
}
