using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public DateTimeOffset? CompletedUtc { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        public string CreatedBy { get; set; }

        [DefaultValue(false)]
        public bool BeingAddressed { get; set; }

        public virtual IEnumerable<Attachment> Attachments { get; set; } = new List<Attachment>();
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        /*public IEnumerable<string> Editors { get; set; }*/

    }
}
