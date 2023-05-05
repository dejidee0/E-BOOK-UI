using MODEL.Entity;

namespace MODEL.DTO
{
    public class PaginatedBooks
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
