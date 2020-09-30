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

        // GET DETAILS
        public ActionResult Details(int id)
        {
            var svc = CreateChoreUserService();
            var model = svc.GetChoreUserById(id);

            return View(model);
        }

        // PUT
        public ActionResult Edit(int id)
        {
            var service = CreateChoreUserService();
            var detail = service.GetChoreUserById(id);
            var model =
                new ChoreUserEdit
                {
                    ChoreUserId = detail.ChoreUserId,
                    UserId = detail.UserId,
                    ChoreId = detail.ChoreId,
                    ChoreIsComplete = detail.ChoreIsComplete
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ChoreUserEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ChoreUserId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateChoreUserService();

            if (service.UpdateChoreUser(model))
            {
                TempData["SaveResult"] = "Chore User was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Chore User could not be updated.");
            return View(model);
        }

        // DELETE
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateChoreUserService();
            var model = svc.GetChoreUserById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChoreUser(int id)
        {
            var service = CreateChoreUserService();

            service.DeleteChoreUser(id);

            TempData["SaveResult"] = "Chore User was deleted";

            return RedirectToAction("Index");
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