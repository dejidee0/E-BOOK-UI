using E_BOOK.UI.Service.Interface;
using MODEL.DTO;
using MODEL.Entity;
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

        public async Task<bool> CreateBookAsync(CreateBookDTO newBook)
        {
            try
            {
                string token = await _localStorage.GetItemAsStringAsync("token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                var result = await _httpClient.PostAsJsonAsync("api/book/add", newBook);
                return true;
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
            
        }

        

        public async Task<bool> DeleteAsyncBook(int id)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var result = await _httpClient.DeleteAsync($"api/book/delete/{id}");
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Book> GetBookAsync(int id)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var result = await _httpClient.GetFromJsonAsync<Book>($"api/book/{id}");
            return result;
        }

        public async Task<PaginatedBooks> GetPaginatedBookAsync(int pageNumber, int perPageSize)
        {  
            var result = await _httpClient.GetFromJsonAsync<PaginatedBooks>($"api/book?pageNumber={pageNumber}&perPageSize={perPageSize}");
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

        public async Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SearchBookDTO>> SearchTitle(string title)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBookAsync(int id, UpdateBookDTO updateBook)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var result = await _httpClient.PutAsJsonAsync($"api/book/update/{id}", updateBook);
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        public async Task<string> UploadImage(int id, IFormFile file)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            using (var content = new MultipartFormDataContent())
            {
                using (var stream = file.OpenReadStream())
                {
                    var streamContent = new StreamContent(stream);
                    content.Add(streamContent, "file", file.FileName);
                    var rest = await _httpClient.PatchAsync($"api/book/photos/{id}", content);
                    var result = await rest.Content.ReadAsStringAsync();
                    if (rest.IsSuccessStatusCode)
                    {
                        return result;
                    }
                    throw new Exception(await rest.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
