using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;

namespace E_BOOK.UI.Pages
{
    public partial class UserRole
    {
        public UserRoleDetail userRoleDetail { get; set; } = new UserRoleDetail();

        [Inject]
        IAccountHttpService accountHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public async void HandleRole()
        {
            var updateRole = await accountHttpService.UpdateUserRole(userRoleDetail.email, userRoleDetail.role);
            if (updateRole)
            {
                navigationManager.NavigateTo("/admin-dashboard");
            }
        }
    }
}
