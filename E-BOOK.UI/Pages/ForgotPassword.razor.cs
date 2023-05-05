using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;

namespace E_BOOK.UI.Pages
{
    public partial class ForgotPassword
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IAccountHttpService _accountHttpService { get; set; }
        private string registerEmail { get; set; }

        public async void HandleForgotPassword() { 
        var result =await _accountHttpService.ForgotPassword(registerEmail);
            if (result)
            {
                Navigation.NavigateTo("/reset-password");
            }

        }
    }
}
