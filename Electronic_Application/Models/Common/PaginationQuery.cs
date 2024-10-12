namespace Electronic_Application.Models.Common
{
    public class PaginationQuery
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; }

        public int SkipPageCount => PageIndex * PageSize;
    }
}
