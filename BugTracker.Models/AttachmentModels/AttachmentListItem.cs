using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Data.Entities;

namespace BugTracker.Models.AttachmentModels
{
    public class AttachmentListItem
    {
       
        public int Id { get; set; }

        public int TicketId { get; set; }
        public string TicketName { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
