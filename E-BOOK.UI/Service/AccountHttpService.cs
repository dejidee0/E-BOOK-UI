using DATA.DTO;
using E_BOOK.UI.Service.Interface;
using MODEL.DTO;
using MODEL.Entity;
using System.Net.Http.Headers;

namespace E_BOOK.UI.Service
{
    public class AccountHttpService : IAccountHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider AuthStateProvider;

        public AccountHttpService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider AuthStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            this.AuthStateProvider = AuthStateProvider;
        }
        public async Task<string> LoginAsyc(SignInModel signInModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/login", signInModel);
            var token = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("token", token);
                await _localStorage.SetItemAsync("email", signInModel.Email);
                await AuthStateProvider.GetAuthenticationStateAsync();
                return token;
            }
            throw new Exception("Login not successful");
        }

        public async Task<bool> SignUpAsync(SignUp signUp)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/account/signup", signUp);
            var response = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("email", signUp.Email);
                return true;
            }
            throw new Exception(response);
        }
        public async Task<bool> ConfirmAccount(ConfirmEmailDTO confirmEmailDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/confirm-email", confirmEmailDTO);
            var response = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response);
        }
        public async Task<bool> ForgotPassword(string email)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/account/forgot-password?Email={email}", "");
            var response = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response);
        }
        public async Task<bool> ResetPassword(ResetPassword reset)
        {

            var result = await _httpClient.PostAsJsonAsync("api/account/reset-password", reset);
            var response = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response);
        }
        public async Task<string> UploadProfielPic(IFormFile file, string email)
        {

            using (var content = new MultipartFormDataContent())
            {
                
                string token = await _localStorage.GetItemAsStringAsync("token");
                using (var stream = file.OpenReadStream())
                {
                    var streamContent = new StreamContent(stream);
                    content.Add(streamContent, "file", file.FileName);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                    var rest = await _httpClient.PatchAsync($"api/account/upload?email={email}", content);
                    var result = await rest.Content.ReadAsStringAsync();
                    if (rest.IsSuccessStatusCode)
                    {
                        return result;
                    }
                    throw new Exception("error while uploading image");
                }
            }
        }

        public async Task<DisplayFindUserDTO> FindUserByAsyc(string email)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var user = await _httpClient.GetFromJsonAsync<DisplayFindUserDTO>($"api/account/search/email?email={email}");
            return user;
        }

        public async Task<PaginatedUser> GetAllUserAsyc(int pageNumber, int perPageSize)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var user = await _httpClient.GetFromJsonAsync<PaginatedUser>($"api/account/all?pageNumber={pageNumber}&perPageSize={perPageSize}");
            return user;
        }

        public async Task<bool> DeleteUserByEmailAsyc(string email)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var user = await _httpClient.DeleteAsync($"api/account/delete/{email}");
            if (user.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserByEmailAsyc(string email, UpdateUserDTO updateUserDTO)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var update = await _httpClient.PutAsJsonAsync($"api/account/update/{email}", updateUserDTO);
            if (update.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserRole(string email, string role)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            var updateRole = await _httpClient.PatchAsync($"api/account/update_role?email={email}&role={role}",null);
            if (updateRole.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
