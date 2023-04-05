using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
    }
}
