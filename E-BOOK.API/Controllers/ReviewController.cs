using DTOs.ReviewDTO;
using E_BOOK.API.Repository.Repository_Interface;
using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_BOOK.API.Controllers
{
	[Route("api/review")]
	[ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReviewController : ControllerBase
	{
		private readonly IReviewRepository _reviewService;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IReviewRepository reviewService, ILogger<ReviewController> logger)
		{
			_reviewService = reviewService;
            _logger = logger;
        }

		[HttpGet("book/{bookId}")]
		public async Task<IActionResult> GetBookReview(int bookId)
		{
			try
			{
                var result = await _reviewService.GetReviewedBookIdAsync(bookId);
				if(result.Data.Count > 0)
				{
                    var averageRating = result.Data.Sum(x => x.Rating) / result.Data.Count();
                    result.AverageReview = averageRating;
					return Ok(result);
				}
				else
				{
                    return Ok(new
					{
						Error = result.error,
						Data = result.Data
                    });
                }

            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "An error occurred while processing get book review by id request");
                return	BadRequest(ex.Message);
			}
			
		}
        [Authorize(Roles = "USER")]
        [HttpPost("book/add")]
		public async Task<IActionResult> AddReview(AddReview addReview)
		{
			try
			{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _reviewService.AddReviewAsync(addReview));
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "An error occurred while processing add review to book request");
                return BadRequest(ex.Message);
			}
			
			
		}

	}
}
