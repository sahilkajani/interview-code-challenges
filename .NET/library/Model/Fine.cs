namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; }

        public DateTime FineDate { get; set; }

        public required string BookLoaned { get; set; }

        public double Amount { get; set; }

        public Guid BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
    }
}
