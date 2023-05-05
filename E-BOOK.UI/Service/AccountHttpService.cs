using DATA.DTO;
using E_BOOK.UI.Service.Interface;
using MODEL.DTO;
using MODEL.Entity;

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
        public async Task<bool> ResetPassword(ResetPassword reset) {

            var result = await _httpClient.PostAsJsonAsync("api/account/reset-password", reset);
            var response = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response);  
        }
        public async Task<string> UploadProfielPic(IFormFile file)
        {

            using (var content = new MultipartFormDataContent())
            {
                var email = await _localStorage.GetItemAsStringAsync("email");
                using (var stream = file.OpenReadStream())
                {
                    var streamContent = new StreamContent(stream);
                    content.Add(streamContent, "file", file.FileName);
                    var rest = await _httpClient.PatchAsync($"users/upload?email={email}", content);
                    var result = await rest.Content.ReadAsStringAsync();
                    if (rest.IsSuccessStatusCode)
                    {
                        await _localStorage.RemoveItemAsync("email");
                    }
                    return result;
                }
            }
        }
    }
}
