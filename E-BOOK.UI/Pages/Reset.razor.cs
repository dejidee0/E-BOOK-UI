using DATA.DTO;
using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class Reset
    {
        public ResetPassword passwordReset = new ResetPassword();
        [Inject]
        IAccountHttpService accountHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        ILocalStorageService localStorageService { get; set; }
        
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var localstorage = await localStorageService.GetItemAsStringAsync("token");
            

            if (localstorage == null)
            {
                navigationManager.NavigateTo("/reset-password");
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }
        public async void HandleReset()
        {
            try
            {
                var result = await accountHttpService.ResetPassword(passwordReset);
                if (result)
                {
                    navigationManager.NavigateTo("/login");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;  
            }
            
            
        }
    }
}

