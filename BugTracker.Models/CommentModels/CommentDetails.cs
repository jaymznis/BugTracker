using BugTracker.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.CommentModels
{
    public class CommentDetails
    {
        public int Id { get; set; }

        [Display(Name = "Comment")]
        public string Content { get; set; }
        public string Commentby { get; set; }


        [Display(Name = "Posted")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Last Edit")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public int TicketId { get; set; }

        public string TicketName { get; set; }
    }
}
