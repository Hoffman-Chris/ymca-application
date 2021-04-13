using System;
using System.ComponentModel.DataAnnotations;

namespace ymca_application.Models
{
    public class ProgramParticipantsModel
    {
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}