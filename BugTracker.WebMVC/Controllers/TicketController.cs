using BugTracker.Models.Ticket;
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
            var model = new TicketListItem[0];
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
            if(ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}