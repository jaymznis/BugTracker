using BugTracker.Models.CommentModels;
using BugTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.WebMVC.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            var model = service.GetCommentsByTicketId(id);
            return View(model);
        }
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;
        }
        public ActionResult Create(int id)
        {
            var model =
                new CommentCreate()
                {
                    TicketId = id
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCommentService();


            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Your Comment was created.";
                return RedirectToAction("../Ticket/Index");
            };

            ModelState.AddModelError("", "Comment could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateCommentService();
            var detail = svc.GetCommentById(id);
            var model = new CommentEdit
            {
                Id = detail.Id,
                TicketId = detail.TicketId,
                Content = detail.Content,
                ModifiedUtc = DateTimeOffset.Now
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCommentService();

            if (service.UserUpdateComment(model))
            {
                TempData["SaveResult"] = "Your Comment was updated.";
                return RedirectToAction($"Details/{id}");
            }

            ModelState.AddModelError("", "Your Comment could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCommentService();

            service.DeleteComment(id);

            TempData["SaveResult"] = "Your Comment was deleted.";

            return RedirectToAction("../Ticket/Index");
        }
    }
}