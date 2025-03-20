namespace OneBeyondApi.DataAccess.OnLoan
{
    public interface IOnLoanRepository
    {
        IEnumerable<OneBeyondApi.Model.OnLoan> GetBooksOnLoan();
    }
}
