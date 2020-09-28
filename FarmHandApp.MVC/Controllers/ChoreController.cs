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
                TempData["SaveResult"] = "Chore was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Chore could not be created.");

            return View(model);
        }

        // GET -- Chore by id
        public ActionResult Details(int id)
        {
            var svc = CreateChoreService();
            var model = svc.GetChoreById(id);

            return View(model);
        }

        // PUT -- Chore by id
        public ActionResult Edit(int id)
        {
            var service = CreateChoreService();
            var detail = service.GetChoreById(id);
            var model =
                new ChoreEdit
                {
                    ChoreId = detail.ChoreId,
                    ChoreName = detail.ChoreName,
                    ChoreDescription = detail.ChoreDescription,
                    Location = detail.Location,
                    Animal = detail.Animal,
                    TimeOfDay = detail.TimeOfDay,
                    IsDaily = detail.IsDaily
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ChoreEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ChoreId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateChoreService();

            if (service.UpdateChore(model))
            {
                TempData["SaveResult"] = "Chore was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Chore could not be updated.");
            return View(model);
        }

        // DELETE -- Chore by id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateChoreService();
            var model = svc.GetChoreById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChore(int id)
        {
            var service = CreateChoreService();

            service.DeleteChore(id);

            TempData["SaveResult"] = "Chore was deleted";

            return RedirectToAction("Index");
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