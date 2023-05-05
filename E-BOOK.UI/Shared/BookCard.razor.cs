using Microsoft.AspNetCore.Components;

namespace E_BOOK.UI.Shared
{
    public partial class BookCard
    {
        [Parameter]
        public string BookImg { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Author { get; set; }
        [Parameter]
        public int Ratings { get; set; } = 0;
        [Parameter]
        public string Availability { get; set; }
        [Parameter]
        public string bookUrl { get; set; }
    }
}
