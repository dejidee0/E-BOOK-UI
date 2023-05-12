
using DTOs.ReviewDTO;
using E_BOOK.API.Repository.Repository_Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MODEL;
using MODEL.Entity;

namespace E_BOOK.API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly E_BookDbContext _bookDbContext;
        private readonly UserManager<AppUser> _userManager;

        public ReviewRepository(E_BookDbContext bookDbContext, UserManager<AppUser> userManager)
        {
            _bookDbContext = bookDbContext;
            _userManager = userManager;
        }

        public async Task<ReviewResponse> GetReviewedBookIdAsync(int bookId)
        {
            var ReviewResponse = new ReviewResponse();
            var findbook = await _bookDbContext.Books.FindAsync(bookId);
            if (findbook == null)
                throw new Exception("Book does not exist");

            var result = await _bookDbContext.Reviews.Where(r => r.BookId == bookId).ToListAsync();
            if (result.Count == 0)
            {
                ReviewResponse.error = "There is no review for the book";
                return ReviewResponse;
            }
            ReviewResponse.Data = result;
            return ReviewResponse;
        }
        public async Task<bool> AddReviewAsync(AddReview model)
        {
            var checkUser = await _userManager.FindByIdAsync(model.AppUserId);
            if (checkUser == null)
            {
                throw new Exception("The user to add review to book not available");
            }
            var checkBook = await _bookDbContext.Books.Include(x=>x.Reviews).FirstOrDefaultAsync(x=>x.Id == model.BookId);
          
            if (checkBook == null)
            {
                throw new Exception("The book to add review is not available");
            }
            var averageRating = (checkBook.Reviews.Sum(x => x.Rating) + model.Rating) / (checkBook.Reviews.Count() + 1);
            checkBook.Rating = averageRating;
            _bookDbContext.Update(checkBook);
            var review = new Review()
            {
                BookId = model.BookId,
                AppUserId = model.AppUserId,
                Title = model.Title,
                Comment = model.Comment,
                DateCreated = model.DateCreated,
                Rating = model.Rating,
            };
            await _bookDbContext.Reviews.AddAsync(review);

            if (await _bookDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Review>> GetReviewsAsync()
        {
            return await _bookDbContext.Reviews.ToListAsync();
        }

    }
}
