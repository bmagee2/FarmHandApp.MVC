using FarmHandApp.Models;
using FarmHandApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmHandApp.MVC.Controllers
{
    [Authorize] // must be logged in
    public class ChoreController : Controller
    {
        // GET: Chore
        public ActionResult Index()
        {
            var service = CreateChoreService();
            var model = service.GetAllChores();

            return View(model);
        }


        // GET -- Create
        public ActionResult Create()
        {
            return View();
        }

        // POST -- Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChoreCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateChoreService();

            if (service.CreateChore(model))
            {
                TempData["SaveResult"] = "Your chore was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The chore could not be created.");

            return View(model);
        }

        // GET -- Chore by id
        public ActionResult Details(int id)
        {
            var svc = CreateChoreService();
            var model = svc.GetChoreById(id);

            return View(model);
        }

        // CreateChoreService METHOD
        private ChoreService CreateChoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChoreService(userId);
            return service;
        }
    }
}