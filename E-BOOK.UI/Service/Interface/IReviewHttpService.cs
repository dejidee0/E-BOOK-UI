using DTOs.ReviewDTO;

namespace E_BOOK.UI.Service.Interface
{
    public interface IReviewHttpService
    {
        Task<ReviewResponse> ReviewResponse(int bookid);
    }
}
