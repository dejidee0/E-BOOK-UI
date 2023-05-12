using DTOs.ReviewDTO;
using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;

namespace E_BOOK.UI.Pages
{
    public partial class AddBookReview
    {
        public AddReview addReview { get; set; } = new AddReview();
        [Parameter]
        public string userid { get; set; }
        [Parameter] 
        public string bookid { get; set; }
        [Inject]
        IReviewHttpService reviewHttpService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        public async void HandleReview() {
            addReview.BookId =int.Parse(bookid);
            addReview.AppUserId = userid;
            var sendReview = await reviewHttpService.AdddReview(addReview);
            if (sendReview)
            {
                navigationManager.NavigateTo($"/singlebook/{bookid}");
            }
        }
    }
}
