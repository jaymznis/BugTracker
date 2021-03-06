using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.CommentModels
{
   public class CommentEdit
    {
        [Display(Name = "Comment")]
        public int Id { get; set; }

        [Display(Name = "Ticket #")]
        public int TicketId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(500, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Comment Info")]
        public string Content { get; set; }

        [Display(Name = "Last Edit")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
