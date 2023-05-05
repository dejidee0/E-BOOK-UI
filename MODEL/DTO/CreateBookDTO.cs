namespace MODEL.DTO
{
    public class CreateBookDTO
    {
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NoPage { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public DateTime AddedToLib { get; set; }
        public string Publisher { get; set; }
        public DateTime PublisherDate { get; set; }
        public int ViewBook { get; set; }
    }
}
