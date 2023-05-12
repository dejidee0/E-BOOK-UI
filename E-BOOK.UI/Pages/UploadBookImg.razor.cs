using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace E_BOOK.UI.Pages
{
    public partial class UploadBookImg
    {
        [Parameter]
        public string Id { get; set; }
        public string UploadUrl { get; set; }

        private IBrowserFile file;
        public IFormFile files;
        private string fileName;
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IBookHttpService bookHttpService { get; set; }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            file = e.File;
            fileName = file?.Name ?? string.Empty;

            using (var stream = file.OpenReadStream())
            {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                files = new FormFile(memoryStream, 0, memoryStream.Length, file.Name, file.Name);
            }

        }
        public async void UploadImage()
        {
            var upload = await bookHttpService.UploadImage(int.Parse(Id), files);
            if (upload != null)
            {
                navigationManager.NavigateTo("/admin-dashboard/book", true);
            }
        }
    }
}
