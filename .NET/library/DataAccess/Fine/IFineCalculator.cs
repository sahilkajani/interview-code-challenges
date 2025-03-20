using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Fine
{
    public interface IFineCalculator
    {
        double Calculate(BookStock bookStock);
    }
}
