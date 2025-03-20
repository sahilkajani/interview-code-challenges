using System.Text.Json.Serialization;

namespace OneBeyondApi.Model
{
    public class Reservation
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime? ReservedFrom { get; set; }
        public DateTime ReservedTo { get; set; }
        public Guid BorrowerId { get; set; }
    }
}
