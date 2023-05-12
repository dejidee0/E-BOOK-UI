using E_BOOK.UI.Service.Interface;
using E_BOOK.UI.Shared;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class ViewBook
    {
        [Parameter]
        public string Id { get; set; }
        public Book _book { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
       private IBookHttpService bookHttpService { get; set; }
        protected ConfirmDelete DeleteConfirmation { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _book = await bookHttpService.GetBookAsync(int.Parse(Id));
        }
        public void DeleteContact()
        {
            DeleteConfirmation.Show();
        }
        public async Task ConfirmDelete_CLick(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                var result = await bookHttpService.DeleteAsyncBook(int.Parse(Id));

                if (result)
                {
                    navigationManager.NavigateTo("/admin-dashboard/book");
                }

            }
        }
    }
}
