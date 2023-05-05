using DTOs.ReviewDTO;
using E_BOOK.UI.Service.Interface;
using Microsoft.AspNetCore.Components;
using MODEL.Entity;

namespace E_BOOK.UI.Pages
{
    public partial class SingleBook
    {
        [Parameter]
        public string Id { get; set; }
        public Book Book { get; set; }
		public ReviewResponse Response { get; set; }
		[Inject]
		IBookHttpService _bookHttpService { get; set; }
		[Inject]
		IReviewHttpService _reviewHttpService { get; set; }
		protected override async Task OnInitializedAsync()
		{
			Book = await _bookHttpService.GetBookAsync(int.Parse(Id));
			Response = await _reviewHttpService.ReviewResponse(int.Parse(Id));
		}
	}
}
