namespace InStudio.Filters.Requests.ProjectOffer
{
    public sealed record ProjectOfferFilterRequest
    {
        public string? CoverLetter { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
