using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;
using System.Diagnostics.Contracts;

namespace E_BOOK.UI.Pages
{
    public partial class Index
    {
        protected PaginatedBooks _Book1 { get; set; }
        protected PaginatedBooks _Book2 { get; set; }
        protected PaginatedBooks _Book3 { get; set; }
        protected PaginatedBooks _Book4 { get; set; }
        protected int PageSize = 6;
        protected int CurrentPage = 1;
        protected int PageSize2 = 6;
        protected int CurrentPage2 = 2;
        public string Query { get; set; }
        public string CssStylePagination { get; set; }
        public string CssRefreshPagination { get; set; } = "d-none";
        [Inject]
        IBookHttpService _bookHttpService {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPage();
            await LoadPage2();
            await LoadPage3();
            await LoadPage4();
        }
        protected async Task LoadPage()
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            _Book1 = await _bookHttpService.GetPaginatedPopularBookAsync(CurrentPage, PageSize);
        }
        
        protected async Task LoadPage2()
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            _Book2 = await _bookHttpService.GetPaginatedPopularBookAsync(CurrentPage2, PageSize2);
        }
        protected async Task LoadPage3()
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            _Book3 = await _bookHttpService.RecentBooks(CurrentPage, PageSize);
        }
        public async Task HandleNext1()
        {
            _Book3 = await _bookHttpService.RecentBooks(CurrentPage++, PageSize);
        }
        public async Task HandlePrev1()
        {
            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            CurrentPage--;
            _Book3 = await _bookHttpService.RecentBooks(CurrentPage, PageSize);
        }
        protected async Task LoadPage4()
        {
            CssStylePagination = "d-block";
            CssRefreshPagination = "d-none";
            _Book4 = await _bookHttpService.RecentBooks(CurrentPage2, PageSize2);
        }
    }
}
