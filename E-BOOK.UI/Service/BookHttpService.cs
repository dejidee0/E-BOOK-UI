using E_BOOK.UI.Service.Interface;
using MODEL.DTO;
using MODEL.Entity;
using System.Net.Http;
using System.Net.Http.Headers;

namespace E_BOOK.UI.Service
{
    public class BookHttpService : IBookHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public BookHttpService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public Task<bool> CreateContactAsync(CreateBookDTO newBook)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookAsync(int id)
        {
			string token = await _localStorage.GetItemAsStringAsync("token");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
			var result = await _httpClient.GetFromJsonAsync<Book>($"api/book/{id}");
			return result;
		}

        public async Task<PaginatedBooks> GetPaginatedPopularBookAsync(int pageNumber, int perPageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PaginatedBooks>($"api/book/popular?pageNumber={pageNumber}&perPageSize={perPageSize}");
            return result;
        }

        public async Task<PaginatedBooks> RecentBooks(int pageNumber, int perPageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PaginatedBooks>($"api/book/recent?pageNumber={pageNumber}&perPageSize={perPageSize}");
            return result;
        }

        public Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SearchBookDTO>> SearchTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBookAsync(int id, UpdateBookDTO updateBook)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadImage(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
