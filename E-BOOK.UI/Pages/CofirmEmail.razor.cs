using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MODEL.DTO;

namespace E_BOOK.UI.Pages
{
    public partial class CofirmEmail
    {
        public string email { get; set; }
        public string token { get; set; }
        public string successpage { get; set; } = "null";
        public string response { get; set; }
        public string errorpage { get; set; } = "d-none";
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IAccountHttpService _accountHttpService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(Navigation.Uri);
            var queryParams = QueryHelpers.ParseQuery(uri.Query);
            if (queryParams.TryGetValue("email", out var emailValue))
            {
                email = emailValue;
            }

            if (queryParams.TryGetValue("token", out var tokenValue))
            {
                var dummyToken = tokenValue.ToString();
                var dummSplit = dummyToken.Split(' ');
                var dumm = string.Join("+", dummSplit);
                token = dumm;
            }
            try
            {
                var data = new ConfirmEmailDTO()
                {
                    email = email,
                    token = token
                };
                var result = await _accountHttpService.ConfirmAccount(data);
                if (result)
                {
                    successpage = "null";
                    response = "You email is confirm successfully";
                }

            }
            catch (Exception ex)
            {
                errorpage = "null";
                response += ex.Message;
            }


        }
    }
}
