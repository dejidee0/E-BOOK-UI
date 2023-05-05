using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class Register
    {
        SignUp user = new SignUp();
     
        [Inject]
        IAccountHttpService accountHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        ILocalStorageService localStorageService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var localstorage = await localStorageService.GetItemAsStringAsync("token");
            if (localstorage == null)
            {
                navigationManager.NavigateTo("/signup");
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }
        public async void HandleRegister()
        {
            var signUp = await accountHttpService.SignUpAsync(user);
            if (signUp)
            {
                navigationManager.NavigateTo("/success");
            }
        }
    }
}
