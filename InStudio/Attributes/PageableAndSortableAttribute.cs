namespace InStudio.Attributes
{
    /// <summary>
    /// Adds pageNumber, pageSize, sortBy and sortDirection attributes to the Swagger definition.
    /// </summary>
    /// <remarks>
    /// To make it work, get the params using the methods in the HttpRequestHelper and adjust the controllers and services to pass
    /// them on.
    /// NOTE: only works for get methods, will be ignored if applied to anything else
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PageableAndSortableAttribute : Attribute
    {
        public PageableAndSortableAttribute(int defaultPageSize = Constants.Page.DefaultPageSize)
        {
            DefaultPageSize = defaultPageSize;
        }

        public int DefaultPageSize { get; }
    }
}