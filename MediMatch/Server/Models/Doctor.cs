using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MediMatch.Server.Models;

namespace MediMatch.Shared
{
    public class Doctor
    {
      
        [Key]
        public string ApplicationUserId { get; set; }

        public string Description { get; set; }
        public string Availability { get; set; }

        public int Rates { get; set; }
        public bool AcceptsInsurance { get; set; }
        public ApplicationUser ApplicationUser { get; set; }



    }
}
