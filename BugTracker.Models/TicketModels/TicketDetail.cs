using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.TicketModels
{
    public class TicketDetail
    {
        [Display(Name = "Ticket #")]
        public int Id { get; set; }

        [Display(Name = "Ticket Name")]
        public string Name { get; set; }

        [Display(Name = "Ticket Info")]
        public string Content { get; set; }

        [Display(Name = "Time Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Last Edit")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        [Display(Name = "Time Completed")]
        public DateTimeOffset? CompletedUtc { get; set; }

        [Display(Name = "Creator")]
        public string CreatedBy { get; set; }

        public bool BeingAddressed { get; set; }

        /*public IEnumerable<string> Editors { get; set; }*/

    }
}
