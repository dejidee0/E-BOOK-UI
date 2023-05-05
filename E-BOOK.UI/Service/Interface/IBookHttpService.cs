using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.UI.Service.Interface
{
    public interface IBookHttpService
    {
        Task<PaginatedBooks> GetPaginatedPopularBookAsync(int pageNumber, int perPageSize);
        Task<PaginatedBooks> RecentBooks(int pageNumber, int perPageSize);
        Task<Book> GetBookAsync(int id);
        Task<Book> UpdateBookAsync(int id, UpdateBookDTO updateBook);
        Task<bool> CreateContactAsync(CreateBookDTO newBook);
        Task<bool> DeleteContactAsync(int id);
        Task<IEnumerable<SearchBookDTO>> SearchTitle(string title);
        Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author);
        Task<bool> DeleteBook(int id);
        Task<string> UploadImage(int id, IFormFile file);
    }
}
