using BugTracker.Data;
using BugTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using BugTracker.Models.TicketModels;
using BugTracker.Models.CommentModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Services
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
                            Id = e.Id,
                            Name = e.Name,
                            CreatedUtc = e.CreatedUtc,
                            CreatedBy = e.CreatedBy,
                            BeingAddressed = e.BeingAddressed,
                            Completed = e.Complete,
                            UserIsAdmin = true
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
                            Id = e.Id,
                            Name = e.Name,
                            CreatedUtc = e.CreatedUtc,
                            CreatedBy = e.CreatedBy,
                            BeingAddressed = e.BeingAddressed,
                            UserIsAdmin = false
                        }
                        );
                return query.ToArray();
            }

        }
        public TicketDetail GetTicketById(int id)
        {
            bool isAdmin = UserIsAdmin(_userId.ToString());
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
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        CreatedBy = entity.CreatedBy,
                        BeingAddressed = entity.BeingAddressed,
                        Complete = entity.Complete,
                        CompletedBy = entity.CompletedBy,
                        CompletedUtc = entity.CompletedUtc,
                        UserIsAdmin = isAdmin,
                        Comments = entity.Comments
                        .Where(e=> entity.Id == e.TicketId)
                       .Select(e => new CommentListItem()
                       {
                           Id = e.Id,
                           Content = e.Content,
                           Commentby = e.Commentby
                       }).ToList()
                    };
            }
        }
        public bool UserUpdateTicket(TicketEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tickets
                    .Single(e => e.Id == model.Id && e.CreatorId == _userId);

                entity.Name = model.Name;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool AdminUpdateTicket(TicketEditAdmin model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tickets
                    .Single(e => e.Id == model.Id);

                entity.Name = model.Name;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.BeingAddressed = model.BeingAddressed;
                entity.Complete = model.Complete;
                if (model.Complete) 
                {
                    entity.CompletedUtc = DateTimeOffset.UtcNow;
                    entity.CompletedBy = _currentUserName;
                }

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UserDeleteTicket(int iD)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                   .Tickets
                   .Single(e => e.Id == iD && e.CreatorId == _userId);

                ctx.Tickets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool AdminDeleteTicket(int iD)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                   .Tickets
                   .Single(e => e.Id == iD);

                ctx.Tickets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UserIsAdmin(string userid)
        {
            
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roles = UserManager.GetRoles(userid);

            if (roles.Contains("admin"))
            {

                return true;
            }
            return false;

          
        }

    }
}
