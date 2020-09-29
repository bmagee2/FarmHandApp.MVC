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
    public class UserChoreController : Controller
    {
        // GET: UserChore
        public ActionResult Index()
        {
            return View();
        }

        // GET -- Create
        public ActionResult Create()
        {
            return View();
        }

        // POST -- Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserChoreCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateUserChoreService();

            if (service.CreateUserChore(model))
            {
                TempData["SaveResult"] = "UserChore was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "UserChore could not be created.");

            return View(model);
        }

        // GET -- UserChore by id
        public ActionResult Details(int id)
        {
            var svc = CreateUserChoreService();
            var model = svc.GetUserChoreById(id);

            return View(model);
        }

        // PUT -- UserChore by id
        public ActionResult Edit(int id)
        {
            var service = CreateUserChoreService();
            var detail = service.GetUserChoreById(id);
            var model =
                new UserChoreEdit
                {
                    ChoreId = detail.ChoreId,   // needed?
                    UserId = detail.UserId,     // needed?
                    IsComplete = detail.IsComplete
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserChoreEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.UserChoreId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateUserChoreService();

            if (service.UpdateUserChore(model))
            {
                TempData["SaveResult"] = "User Chore was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "User Chore could not be updated.");
            return View(model);
        }

        // DELETE -- UserChore by id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserChoreService();
            var model = svc.GetUserChoreById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChore(int id)
        {
            var service = CreateUserChoreService();

            service.DeleteUserChore(id);

            TempData["SaveResult"] = "User Chore was deleted";

            return RedirectToAction("Index");
        }

        // CreateChoreService METHOD
        private UserChoreService CreateUserChoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserChoreService(userId);
            return service;
        }

    }
}