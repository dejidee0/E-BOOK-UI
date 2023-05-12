using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;

namespace E_BOOK.UI.Pages
{
    public partial class CreateBook
    {
        CreateBookDTO _book = new CreateBookDTO();
        [Parameter]
        public string userid { get; set; }
        [Inject]
        IBookHttpService _bookHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }


        public async void HandleBook()
        {
            _book.AppUserId = userid;
            _book.ViewBook = 0;
            var createBook = await _bookHttpService.CreateBookAsync(_book);
            if (createBook)
            {
                navigationManager.NavigateTo("/admin-dashboard/book");
            }
        }
    }
}
