using Microsoft.AspNetCore.Identity;
using MediMatch.Shared;

namespace MediMatch.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Suffix { get; set; } = null!;
        public DateTime DOB { get; set; }
        public string? Address { get; set; } = null!;
        public string? UserType { get; set; } = null!;
        public ApplicationUserDto ToDto()
        {
            return new ApplicationUserDto
            {
                Id = this.Id,
                FirstName = this.FirstName,
                MiddleInitial = this.MiddleInitial,
                LastName = this.LastName,
                Suffix = this.Suffix,
                DOB = this.DOB,
                Address = this.Address,
                UserType = this.UserType
            };
        }
        public List<Message> MessageTo { get; set; } = null!;
        public List<Message> MessageFrom { get; set; } = null!;

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}