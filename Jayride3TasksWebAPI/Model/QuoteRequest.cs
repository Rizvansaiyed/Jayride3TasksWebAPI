namespace Jayride3TasksWebAPI.Model
{
    public class QuoteRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<ExternalListing> Listings { get; set; }

    }
}
