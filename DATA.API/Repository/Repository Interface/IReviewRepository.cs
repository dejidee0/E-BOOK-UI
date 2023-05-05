using DTOs.ReviewDTO;
using MODEL.Entity;

namespace E_BOOK.API.Repository.Repository_Interface
{
	public interface IReviewRepository
	{
		Task<IEnumerable<Review>> GetReviewsAsync();
		Task<ReviewResponse> GetReviewedBookIdAsync(int bookId);

        Task<bool> AddReviewAsync(AddReview model);
	}
}
