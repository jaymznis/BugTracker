using BugTracker.Data;
using BugTracker.Data.Entities;
using BugTracker.Models.AttachmentModels;
using BugTracker.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BugTracker.Services
{
    public class AttachmentService
    {

        private readonly Guid _userId;

        private readonly string _currentUserName = HttpContext.Current.User.Identity.Name;
        public AttachmentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAttachment(AttachmentCreate model)
        {
            var entity =
                new Attachment()
                {
                    CreatorId = _userId,
                    Attachedby = _currentUserName,
                    URL = model.URL,
                    CreatedUtc = DateTimeOffset.Now,
                    TicketId = model.TicketId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attachments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<AttachmentListItem> GetAttachmentsByTicketId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Attachments
                    .Where(e => e.TicketId == id)
                    .Select(
                        e =>
                        new AttachmentListItem
                        {
                            Id = e.Id,
                            URL = e.URL,
                            TicketId = e.TicketId,
                            TicketName = e.Ticket.Name
                        });
                return query.ToArray();

            }
        }
        public AttachmentDetails GetAttachmentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Attachments
                    .Single(e => e.Id == id);
                return
                    new AttachmentDetails
                    {
                        Id = entity.Id,
                        URL = entity.URL,
                        Attachedby = entity.Attachedby,
                        CreatedUtc = entity.CreatedUtc,
                        TicketId = entity.TicketId,
                        Ticket = new TicketListItem
                        {
                           Name = entity.Ticket.Name
                        }
                    };
            }
        }

        public bool AdminDeleteAttachment(int iD)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                   .Attachments
                   .Single(e => e.Id == iD);

                ctx.Attachments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAttachment(int iD)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                   .Attachments
                   .Single(e => e.Id == iD && e.CreatorId == _userId);

                ctx.Attachments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
