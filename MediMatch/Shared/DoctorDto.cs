using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MediMatch.Shared
{
    public class DoctorDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? Availability { get; set; } = null!;
        public string? Specialty { get; set; } = null!;
        public int Rates { get; set; }
        public bool AcceptsInsurance { get; set; }

    }
}
