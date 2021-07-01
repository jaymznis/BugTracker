using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.CommentModels
{
    public class CommentListItem
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string TicketName { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Content { get; set; }
        public string Commentby { get; set; }
    }
}
