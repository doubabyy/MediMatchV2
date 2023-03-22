using Microsoft.AspNetCore.Identity;

namespace MediMatch.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string MiddleInitial { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Suffix { get; set; } = null!;
        public DateTime DOB { get; set; }
        public string Address { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public List<Message> MessageTo { get; set; } = null!;
        public List<Message> MessageFrom { get; set; } = null!;



    }
}