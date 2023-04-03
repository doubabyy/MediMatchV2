using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MediMatch.Server.Models
{
    public class Patient
    {

        [Key]
        public string ApplicationUserId { get; set; }

        public string? Gender { get; set; } = null!;
        public bool DepAnx { get; set; }

        public string? DepAnxDesc { get; set; } = null!;
        public bool SuicThoughts { get; set; }

        public string? SuicThoughtsDesc { get; set; } = null!;
        public bool SubstanceAbuse { get; set; }
        public string? SubstanceAbuseDesc { get; set; } = null!;

        public string? SupportSystem { get; set; } = null!;
        public bool Therapy { get; set; }
        public string? TherapyDesc { get; set; } = null!;

        public string? ProblemsDesc { get; set; } = null!;

        public string? TreatmentGoals { get; set; } = null!;

        public ApplicationUser ApplicationUser { get; set; }



    }
}
