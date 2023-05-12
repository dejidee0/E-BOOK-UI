using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MODEL.Entity
{
    public class Review
    {
        public int Id { get; set; }
        //[JsonIgnore]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public AppUser AppUser  { get; set; }
        public string AppUserId { get; set; }
        public int Rating { get; set; }
    }
}
