namespace MODEL.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string BookImg { get; set; }
        public string Author { get; set; }
        public int NoPage { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public DateTime AddedToLib { get; set; }
        public string Publisher { get; set; }
        public DateTime PublisherDate { get; set; }
        public List<Review> Reviews { get; set; }
        public double Rating { get; set; }
        public int ViewBook { get; set; }

    }
}
