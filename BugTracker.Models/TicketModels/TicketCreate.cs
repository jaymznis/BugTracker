using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.Ticket
{
    public class TicketCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Ticket Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(10000)]
        [Display(Name = "Ticket Info")]
        public string Content { get; set; }

    }
}
