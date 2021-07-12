using BugTracker.Data;
using BugTracker.Models.TicketModels;
using BugTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var model2 = service.GetAllTickets();

            var admin = service.UserIsAdmin(userId.ToString());

            if (admin)
            {
                return View(model2);
            }

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


        public ActionResult Edit(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svcA = CreateTicketService();
            var detail = svcA.GetTicketById(id);
            var admin = svcA.UserIsAdmin(userId.ToString());
            if (admin)
            {
                var modelA = new TicketEditAdmin
                {
                    Name = detail.Name,
                    Content = detail.Content,
                    ModifiedUtc = DateTimeOffset.Now,
                    BeingAddressed = detail.BeingAddressed,
                    Complete = detail.Complete
                };
                return View(modelA);
            }
            else
            {
                var model = new TicketEdit
                {
                    Name = detail.Name,
                    Content = detail.Content,
                    ModifiedUtc = DateTimeOffset.Now,
                };
                return View(model);
            }
        }

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
                TempData["SaveResult"] = "Ticket was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Ticket could not be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = CreateTicketService();
            var model = svc.GetTicketById(id);
            var admin = svc.UserIsAdmin(userId.ToString());

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = CreateTicketService();
            var admin = service.UserIsAdmin(userId.ToString());

            if (admin)
            {
                service.AdminDeleteTicket(id);

                TempData["SaveResult"] = "Ticket was deleted.";

                return RedirectToAction("Index");
            }
            else
            {
                service.UserDeleteTicket(id);

                TempData["SaveResult"] = "Your Ticket was deleted.";

                return RedirectToAction("Index");
            }
        }
    private TicketService CreateTicketService()
    {
        var userId = Guid.Parse(User.Identity.GetUserId());
        var service = new TicketService(userId);
        return service;
    }
    }
}

