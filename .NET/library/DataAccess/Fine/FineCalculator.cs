using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Fine
{
    public class FineCalculator : IFineCalculator
    {
        public const float FineAmountPerDay = 0.50f;

        private readonly IFineRepository _fineRepository;

        public FineCalculator(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        public double Calculate(BookStock bookStock)
        {
            var numberOfDaysLate = (DateTime.Today - bookStock.LoanEndDate.Value).TotalDays;

            var fine = numberOfDaysLate * FineAmountPerDay;

            return fine;
        }
    }
}
