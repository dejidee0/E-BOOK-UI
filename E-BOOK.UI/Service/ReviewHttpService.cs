using DTOs.ReviewDTO;
using E_BOOK.UI.Service.Interface;
using System.Net.Http.Headers;

namespace E_BOOK.UI.Service
{
    public class ReviewHttpService : IReviewHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ReviewHttpService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task<ReviewResponse> ReviewResponse(int bookid)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var result = await _httpClient.GetFromJsonAsync<ReviewResponse>($"api/review/book/{bookid}");
            return result;
        }
    }
}
