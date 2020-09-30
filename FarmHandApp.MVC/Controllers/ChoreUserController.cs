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
    public class ChoreUserController : Controller
    {
        // GET: ChoreUser
        public ActionResult Index()
        {
            var service = CreateChoreUserService();
            var model = service.GetAllChoreUsers();

            return View(model);
            //return View();
        }

        // GET -- Create
        public ActionResult Create()
        {
            return View();
        }

        // POST -- Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChoreUserCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateChoreUserService();

            if (service.CreateChoreUser(model))
            {
                TempData["SaveResult"] = "Chore User was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Chore User could not be created.");

            return View(model);
        }


        // CreateChoreUserService METHOD
        private ChoreUserService CreateChoreUserService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChoreUserService(userId);
            return service;
        }
    }
}