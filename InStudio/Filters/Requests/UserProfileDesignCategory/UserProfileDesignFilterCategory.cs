namespace InStudio.Filters.Requests.UserProfileDesignCategory
{
    public class UserProfileDesignFilterCategory
    {
        public Guid? UserId { get; set; }
        public required string Title { get; set; }
        public int? DesignCategoryId { get; set; }
    }
}
