using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
        public string? About { get; set; }
        public string? JobTitle { get; set; }

        public List<Blog>? Blogs { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Message> MessageSender { get; set; }
        public List<Message> MessageReceiver { get; set; }
    }
}
