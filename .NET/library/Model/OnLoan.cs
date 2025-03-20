using System.Text.Json.Serialization;

namespace OneBeyondApi.Model
{
    public class OnLoan
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public required string BookName { get; set; }

        public required string BorrowerName { get; set; }

        public required string BorrowerEmailAddress { get; set; }
    }
}
