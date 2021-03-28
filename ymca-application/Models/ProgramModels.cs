using System;
using System.ComponentModel.DataAnnotations;

namespace ymca_application.Models
{
    public class ProgramModels
    {
        [Display(Name = "ProgramId")]
        public int ProgramId { get; set; }

        [Display(Name = "UserId")]
        public string UserID { get; set; }

        [Display(Name = "StartTime")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "EndTime")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Monday")]
        public bool? Monday { get; set; }

        [Display(Name = "Tuesday")]
        public bool? Tuesday { get; set; }

        [Display(Name = "Wednesday")]
        public bool? Wednesday { get; set; }

        [Display(Name = "Thursday")]
        public bool? Thursday { get; set; }

        [Display(Name = "Friday")]
        public bool? Friday { get; set; }

        [Display(Name = "Saturday")]
        public bool? Saturday { get; set; }

        [Display(Name = "Sunday")]
        public bool? Sunday { get; set; }
    }
}