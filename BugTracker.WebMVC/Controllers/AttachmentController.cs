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
            var model =
                new AttachmentCreate()
                {
                    TicketId = id
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttachmentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAttachmentService();


            if (service.CreateAttachment(model))
            {
                TempData["SaveResult"] = "Your Attachment was added.";
                return RedirectToAction($"../Ticket/Details/{model.TicketId}");
                
            };

            ModelState.AddModelError("", "Could not attach.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAttachmentService();
            var model = svc.GetAttachmentById(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAttachmentService();
            var model = svc.GetAttachmentById(id);

            return View(model);
        }

        [ActionName("AdminDelete")]
        public ActionResult AdminDelete(int id)
        {
            var svc = CreateAttachmentService();
            var model = svc.GetAttachmentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAttachmentService();
           
            var model = service.GetAttachmentById(id);
        
            service.DeleteAttachment(id);

            TempData["SaveResult"] = "Your Attachment was deleted.";

                return RedirectToAction($"../Ticket/Details/{model.TicketId}");
            
        }

        [HttpPost]
        [ActionName("AdminDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AdminDeletePost(int id)
        {
            var service = CreateAttachmentService();

            var model = service.GetAttachmentById(id);

            service.AdminDeleteAttachment(id);

            TempData["SaveResult"] = "Your Attachment was deleted.";

            return RedirectToAction($"../Ticket/Details/{model.TicketId}");

        }


    }
}