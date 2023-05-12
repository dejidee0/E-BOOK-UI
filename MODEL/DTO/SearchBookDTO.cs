using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class SearchBookDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int NoPage { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public DateTime AddedToLib { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public DateTime PublisherDate { get; set; }
    }
}
