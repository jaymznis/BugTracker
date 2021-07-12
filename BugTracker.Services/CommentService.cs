using BugTracker.Data;
using BugTracker.Data.Entities;
using BugTracker.Models.CommentModels;
using BugTracker.Models.TicketModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BugTracker.Services
{
   public class CommentService
    {
        private readonly Guid _userId;

        private readonly string _currentUserName = HttpContext.Current.User.Identity.Name;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    CreatorId = _userId,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    Commentby = _currentUserName,
                    TicketId = model.TicketId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentListItem> GetCommentsByTicketId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Comments
                    .Where(e => e.TicketId == id)
                    .Select(
                        e =>
                        new CommentListItem
                        {
                            Id = e.Id,
                            Content = e.Content,
                            TicketId = e.TicketId,
                            TicketName = e.Ticket.Name,
                            Commentby = e.Commentby
                        });
                return query.ToArray();

            }
        }

        public CommentDetails GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == id);
                return
                    new CommentDetails
                    {
                        Id = entity.Id,
                        Content = entity.Content,
                        Commentby = entity.Commentby,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        TicketId = entity.TicketId,
                        TicketName = entity.Ticket.Name
                    };
            }
        }
        public bool UserUpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == model.Id && e.CreatorId == _userId);

               
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int iD)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx
                   .Comments
                   .Single(e => e.Id == iD && e.CreatorId == _userId);

                ctx.Comments.Remove(entity);

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
