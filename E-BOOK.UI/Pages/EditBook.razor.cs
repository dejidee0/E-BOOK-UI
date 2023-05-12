using E_BOOK.UI.Service.Interface;
using E_BOOK.UI.Shared;
using Microsoft.AspNetCore.Components;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class EditBook
    {
        [Parameter]
        public string Id { get; set; }
        public Book _book { get; set; } = new Book();
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        private IBookHttpService bookHttpService { get; set; }
        protected ConfirmDelete DeleteConfirmation { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _book = await bookHttpService.GetBookAsync(int.Parse(Id));
        }
        public async void HandleSubmited()
        {

            var updateDetails = new UpdateBookDTO
            {
                Title = _book.Title,
                Author = _book.Author,
                Publisher = _book.Publisher,
                PublisherDate = _book.PublisherDate,
                NoPage = _book.NoPage,
                AddedToLib = _book.AddedToLib,
                Description = _book.Description,
                ISBN = _book.ISBN,
            };
            var update = await bookHttpService.UpdateBookAsync(int.Parse(Id), updateDetails);
            if (update)
            {
                navigationManager.NavigateTo("/admin-dashboard/book");
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
                var result = await bookHttpService.DeleteAsyncBook(int.Parse(Id));

                if (result)
                {
                    navigationManager.NavigateTo("/admin-dashboard/book");
                }

            }
        }
    }
}
