using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_BOOK.UI.Pages
{
    public partial class Dashboard
    {
        [Inject]
        private IAccountHttpService accountHttpService { get; set; }
        protected PaginatedUser PaginatedUser { get; set; }
        public string Query { get; set; }
        protected int CurrentPage { get; set; } = 1;
        protected int perPageSize { get; set; } = 3;
        [Inject]
        NavigationManager navigationManager { get; set; }
        public string CssStylePagination { get; set; }
        public string CssRefreshPagination { get; set; } = "d-none";
        [Inject]
        ILocalStorageService localStorageService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadPage();
            
        }
        protected async Task LoadPage()
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            PaginatedUser = await accountHttpService.GetAllUserAsyc(CurrentPage, perPageSize);
        }
        protected async Task LoadPage(int i)
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            CurrentPage = i;
            await LoadPage();
        }
        protected async Task PrevPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadPage();
            }
        }
        protected async Task NextPage()
        {
            if (CurrentPage < PaginatedUser.TotalPages)
            {
                CurrentPage++;
                await LoadPage();
            }

        }
        public async Task SearchBook()
        {
            //if (!string.IsNullOrEmpty(Query))
            //{
            //    var result = await contactHttpService.SearchContactAsync(Query);
            //    if (result.Count() > 0)
            //    {
            //        var user = new PaginatedUser();
            //        contact.Contacts = result;
            //        _Contact = contact;
            //        CssStylePagination = "d-none";
            //        CssRefreshPagination = "d-block";
            //    }
            //}
        }

    }
}
