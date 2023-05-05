using MODEL.Entity;

namespace DTOs.ReviewDTO
{
    public class ReviewResponse
    {
        public List<Review> Data { get; set; } = new List<Review>();
        public string error { get; set; }
        public int AverageReview { get; set; }
    }
}
