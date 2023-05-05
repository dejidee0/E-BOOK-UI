using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReviewDTO
{
	public class AddReview
	{
		[Required] 
		public string AppUserId { get; set; }
		[Required]
		public int BookId { get; set; }
		[Required]
		public string Title { get; set; }

		[Required]
		public string Comment { get; set; }

		[Required]
		[Range(1, 5)]
		public int Rating { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.Now;


	}
}
