using BugTracker.Data;
using BugTracker.Data.Entities;
using BugTracker.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BugTracker.Services.TicketModels
{
    public class TicketService
    {
        private readonly Guid _userId;

        public TicketService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTicket(TicketCreate model)
        {
            var entity =
                new Ticket()
                {
                    CreatorId = _userId,
                    Name = model.Name,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    CreatedBy = HttpContext.Current.GetOwinContext()
    .GetUserManager<ApplicationUserManager>().FindById(_userId).UserName;
        };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tickets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
