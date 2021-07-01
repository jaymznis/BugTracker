using BugTracker.Models.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.AttachmentModels
{
    public class AttachmentDetails
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Attachedby { get; set; }


        [Display(Name = "Posted")]
        public DateTimeOffset CreatedUtc { get; set; }

        public int TicketId { get; set; }

        public virtual TicketListItem Ticket { get; set; }


    }
}
