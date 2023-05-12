using E_BOOK.UI.Service.Interface;
using E_BOOK.UI.Shared;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;

namespace E_BOOK.UI.Pages
{
    public partial class ViewUser
    {
        [Parameter]
        public string email { get; set; }
        public DisplayFindUserDTO displayFindUserDTO { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        private IAccountHttpService accountHttpService { get; set; }
        protected ConfirmDelete DeleteConfirmation { get; set; }
        protected override async Task OnInitializedAsync()
        {
            displayFindUserDTO = await accountHttpService.FindUserByAsyc(email);
        }
        public void DeleteContact()
        {
            DeleteConfirmation.Show();
        }
        public async Task ConfirmDelete_CLick(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                var result = await accountHttpService.DeleteUserByEmailAsyc(email); 

                if (result)
                {
                    navigationManager.NavigateTo("/admin-dashboard"); 
                }

            }
        }
    }
}
