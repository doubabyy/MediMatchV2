﻿using System;
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

        public string Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public ApplicationUser ApplicationUser { get; set; }



    }
}
