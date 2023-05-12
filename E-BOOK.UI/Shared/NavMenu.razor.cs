using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;
using System.Security.Claims;

namespace E_BOOK.UI.Shared
{
    public partial class NavMenu
    {
        
        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; }
       public  DisplayFindUserDTO displayFindUserDTO { get; set; }
        [Inject]
        private IAccountHttpService accountHttpService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var user = await AuthStateProvider.GetAuthenticationStateAsync();
            var email = user.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            Console.WriteLine(user.User.Identity.Name);
            displayFindUserDTO = await accountHttpService.FindUserByAsyc(email);
           
        }
    }
}
