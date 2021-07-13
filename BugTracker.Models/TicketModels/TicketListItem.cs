using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.TicketModels
{
    public class TicketListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc{ get; set; }

        public string CreatedBy { get; set; }

        public bool BeingAddressed { get; set; }

        public bool Completed { get; set; }

        public bool UserIsAdmin { get; set; }

    }
}
