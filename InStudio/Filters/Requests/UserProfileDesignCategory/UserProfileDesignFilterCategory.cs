namespace InStudio.Filters.Requests.UserProfileDesignCategory
{
    public class UserProfileDesignFilterCategory
    {
        public int? UserProfileId { get; set; }
        public required string Title { get; set; }
        public required string Username { get; set; }
        public int? DesignCategoryId { get; set; }
    }
}
