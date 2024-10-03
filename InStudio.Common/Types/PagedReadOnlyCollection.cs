using System.Collections.ObjectModel;

namespace InStudio.Common.Types
{
    public class PagedReadOnlyCollection<T> : ReadOnlyCollection<T>
    {
        public PagedReadOnlyCollection(IList<T> list, long totalCount)
            : base(list)
        {
            TotalCount = totalCount;
        }

        public long TotalCount { get; }
    }
}
