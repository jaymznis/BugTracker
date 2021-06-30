using BugTracker.Data;
using BugTracker.Data.Entities;
using BugTracker.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using BugTracker.Models.TicketModels;

namespace BugTracker.Services.TicketModels
{
    public class TicketService
    {
        private readonly Guid _userId;

        private readonly string _currentUserName = HttpContext.Current.User.Identity.Name;
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
                    CreatedBy = _currentUserName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tickets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TicketListItem> GetAllTickets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Tickets
                    .Select(
                        e =>
                        new TicketListItem
                        {
                           Name = e.Name,
                           CreatedUtc = e.CreatedUtc,
                           CreatedBy = e.CreatedBy,
                           BeingAddressed = e.BeingAddressed
                        }
                        );
                return query.ToArray();
            }

        }
        public IEnumerable<TicketListItem> GetMyTickets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Tickets
                    .Where(e => e.CreatorId == _userId)
                    .Select(
                        e =>
                        new TicketListItem
                        {
                            Name = e.Name,
                            CreatedUtc = e.CreatedUtc,
                            CreatedBy = e.CreatedBy,
                            BeingAddressed = e.BeingAddressed
                        }
                        );
                return query.ToArray();
            }

        }
        public TicketDetail GetTicketById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tickets
                    .Single(e => e.Id == id);
                return
                    new TicketDetail
                    {
                       Id = entity.Id,
                       Name = entity.Name,
                       CreatedUtc = entity.CreatedUtc,
                       CompletedUtc = entity.CompletedUtc,
                       CreatedBy = entity.CreatedBy,
                       BeingAddressed = entity.BeingAddressed
                    };
            }
        }


    }
}
