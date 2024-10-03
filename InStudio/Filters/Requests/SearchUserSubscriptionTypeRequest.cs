namespace InStudio.Filters.Requests
{
    public sealed record SearchUserSubscriptionTypeRequest
    {
        public string? Title { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
    }
}
