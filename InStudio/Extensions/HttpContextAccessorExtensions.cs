using InStudio.Attributes;
using InStudio.Common;
using Microsoft.VisualBasic;

namespace InStudio.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static PageableParams GetPageableParams(this IHttpContextAccessor httpContextAccessor)
        {
            var hasPageNumber = int.TryParse(httpContextAccessor?.HttpContext?.Request.Query[Constants.Page.PageQueryKey], out int pageNumber);
            var hasPageSize = int.TryParse(httpContextAccessor?.HttpContext?.Request.Query[Constants.Page.PageSizeQueryKey], out int pageSize);
            return new PageableParams
            {
                Page = !hasPageNumber || !hasPageSize || pageNumber <= 0 ? 1 : pageNumber,
                Size = !hasPageSize || pageSize <= 0 ? Constants.Page.DefaultPageSize : pageSize,
            };
        }

        public static SortParameter GetSortParams<T>(this IHttpContextAccessor httpContextAccessor)
            where T : class
        {
            var sortBy = httpContextAccessor?.HttpContext?.Request.Query[Constants.Sort.SortByQueryKey];
            if (string.IsNullOrEmpty(sortBy))
            {
                return new SortParameter();
            }

            var sortColumns = GetSortableProperties<T>();
            if (!sortColumns.Any(x => x.Equals(sortBy, StringComparison.Ordinal)))
            {
                return new SortParameter();
            }

            var sortDirection = httpContextAccessor?.HttpContext?.Request.Query[Constants.Sort.SortDirectionQueryKey];
            return new SortParameter
            {
                SortBy = sortBy,
                SortDirection = (string.Equals(sortDirection, Constants.Sort.SortDirectionAsc, StringComparison.OrdinalIgnoreCase) || string.Equals(sortDirection, Constants.Sort.SortDirectionDesc, StringComparison.OrdinalIgnoreCase)) ? sortDirection : Constants.Sort.SortDirectionDesc,
            };
        }

        private static List<string> GetSortableProperties<T>()
            where T : class
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return typeof(T).GetProperties()
                .Where(x => x.IsDefined(typeof(SortableColumnAttribute), false))
                .Select(x => (x.GetCustomAttributes(typeof(SortableColumnAttribute), false)[0] as SortableColumnAttribute)?.Name)
                .ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
