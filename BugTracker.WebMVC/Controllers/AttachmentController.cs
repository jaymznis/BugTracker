using BugTracker.Models.AttachmentModels;
using BugTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.WebMVC.Controllers
{
    [Authorize]
    public class AttachmentController : Controller
    {
       
        // GET: Attachment
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AttachmentService(userId);
            var model = service.GetAttachmentsByTicketId(id);
            return View(model);
        }
        private AttachmentService CreateAttachmentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AttachmentService(userId);
            return service;
        }
        public ActionResult Create(int id)
        {
            var entity =
                new AttachmentCreate()
                {
                    TicketId = id
                };
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttachmentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAttachmentService();


            if (service.CreateAttachment(model))
            {
                TempData["SaveResult"] = "Your Ticket was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Ticket could not be created.");

            return View(model);
        }

        private Guid UserId()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return userId;

        }
    }
}