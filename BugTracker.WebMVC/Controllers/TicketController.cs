using BugTracker.Data;
using BugTracker.Models.Ticket;
using BugTracker.Models.TicketModels;
using BugTracker.Services.TicketModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.WebMVC.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TicketService(userId);
            var model = service.GetMyTickets();
            return View(model);
        }

        //Get
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTicketService();

            if (service.CreateTicket(model))
            {
                TempData["SaveResult"] = "Your Ticket was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Ticket could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTicketService();
            var model = svc.GetTicketById(id);

            return View(model);
        }

        /*[Authorize(Roles = "admin")]*/
        public ActionResult Edit(int id)
        {
         /*   if (User.IsInRole("admin"))
            {*/
                var svcA = CreateTicketService();
                var detailA = svcA.GetTicketById(id);
                var modelA = new TicketEditAdmin
                {
                    Name = detailA.Name,
                    Content = detailA.Content,
                    ModifiedUtc = DateTimeOffset.Now,
                    BeingAddressed = detailA.BeingAddressed
                };
                return View(modelA);
            
           /* var svc = CreateTicketService();
            var detail = svc.GetTicketById(id);
            var model = new TicketEdit
            {
                Name = detail.Name,
                Content = detail.Content,
                ModifiedUtc = DateTimeOffset.Now,
            };
            return View(model);*/
        }
       /* [Authorize(Roles = "admin")]*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TicketEditAdmin model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTicketService();

            if (service.AdminUpdateTicket(model))
            {
                TempData["SaveResult"] = "Your Ticket was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Ticket could nor be updated");
            return View(model);
        }
       /* public ActionResult UserEdit(int id)
        {
          
            var svc = CreateTicketService();
            var detail = svc.GetTicketById(id);
            var model = new TicketEditAdmin
            {
                Name = detail.Name,
                Content = detail.Content,
                ModifiedUtc = DateTimeOffset.Now,
            };
            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(int id, TicketEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTicketService();

            if (service.UserUpdateTicket(model))
            {
                TempData["SaveResult"] = "Your Ticket was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Ticket could nor be updated");
            return View(model);
        }*/
        private TicketService CreateTicketService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TicketService(userId);
            return service;
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTicketService();
            var model = svc.GetTicketById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTicketService();

            service.UserDeleteTicket(id);

            TempData["SaveResult"] = "Your Ticket was deleted.";

            return RedirectToAction("Index");
        }

    }
}
