namespace Area92.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(int currentPage, int pageSize, int totalCount, List<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public int CurrentPage { get; private set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPage);

        public static PagedList<T> create(IQueryable<T> source, int page, int size)
        {
            var count = source.Count();
            var items = source.Skip(size * (page - 1)).Take(size).ToList();
            return new PagedList<T>(page, size, count, items);
        }
    }
}