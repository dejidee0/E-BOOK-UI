using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.API.Repository.Repository_Interface
{
    public interface IE_BookRepository
    {
        Task<bool> RemoveBook(int Id);
        Task<Book> UpdateBookImg(Book book);
        Task<IEnumerable<Book>> GetALLBook();
        Task<Book> GetBookById(int Id);
        PaginatedBooks PaginatedAsync(List<Book> books, int pageNumber, int perPageSize);
        IEnumerable<Book> PopularBook();
        IEnumerable<Book> RecentBook();
        public  Task<bool> CreateBook_IZU(CreateBookDTO createBookDTO);

        public Task<Book> UpdateBook_IZU(int bookId, UpdateBookDTO updateBookDTO);

        public  Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author);

        public Task<IEnumerable<SearchBookDTO>> SearchTitle_IZU(string title);


    }
}
