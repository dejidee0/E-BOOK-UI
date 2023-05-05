
using Microsoft.AspNetCore.Identity;
using MODEL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entity
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public List<Book> Books { get; set; }
        public List<Review> Reviews { get; set; }   
        
    }
}
