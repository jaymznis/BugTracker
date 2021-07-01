using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        public string Commentby { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
