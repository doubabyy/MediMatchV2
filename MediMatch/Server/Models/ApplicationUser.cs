using Microsoft.AspNetCore.Identity;

namespace MediMatch.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Message> MessageTo { get; set; }
        public List<Message> MessageFrom { get; set; }



    }
}