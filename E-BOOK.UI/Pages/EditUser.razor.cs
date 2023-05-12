using E_BOOK.UI.Service.Interface;
using E_BOOK.UI.Shared;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;

namespace E_BOOK.UI.Pages
{
    public partial class EditUser
    {
        [Parameter]
        public string email { get; set; }
        public DisplayFindUserDTO displayFindUserDTO { get; set; } = new DisplayFindUserDTO();
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        private IAccountHttpService accountHttpService { get; set; }
        protected ConfirmDelete DeleteConfirmation { get; set; }
        protected override async Task OnInitializedAsync()
        {
            displayFindUserDTO = await accountHttpService.FindUserByAsyc(email);
        }
        public async void HandleSubmited()
        {

            var updateDetails = new UpdateUserDTO
            {
                Email = displayFindUserDTO.Email,
                FirstName = displayFindUserDTO.FirstName,
                LastName = displayFindUserDTO.LastName,
                PhoneNumber = displayFindUserDTO.PhoneNumber,
                Gender = displayFindUserDTO.Gender,
                UserName = displayFindUserDTO.UserName,
            };
            var update = await accountHttpService.UpdateUserByEmailAsyc(email, updateDetails);
            if (update)
            {
                navigationManager.NavigateTo("/admin-dashboard");
            }
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
