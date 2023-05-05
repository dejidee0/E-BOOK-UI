using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class Login
    {

        SignInModel user = new SignInModel();
        [Inject]
        IAccountHttpService accountHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        ILocalStorageService localStorageService { get; set; }
        //[Inject]
        //protected IHttpContextAccessor HttpContextAccessor { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var localstorage = await localStorageService.GetItemAsStringAsync("token");
            //var user = HttpContextAccessor.HttpContext.User;
            //var role = user.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

            if (localstorage == null)
            {
                navigationManager.NavigateTo("/login");
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }
        public async void HandleLogin()
        {
            var signIn = await accountHttpService.LoginAsyc(user);
            if (signIn != null)
            {
                navigationManager.NavigateTo("/");
            }
        }
    }
}
