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
    public class BulletinController : Controller
    {
        // GET: Bulletin
        public ActionResult Index()
        {
            var service = CreateBulletinService();
            var model = service.GetAllBulletins();

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
        public ActionResult Create(BulletinCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBulletinService();

            if (service.CreateBulletin(model))
            {
                TempData["SaveResult"] = "Bulletin was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Bulletin could not be created.");

            return View(model);
        }

        // DETAILS -- Bulletin by id
        public ActionResult Details(int id)
        {
            var svc = CreateBulletinService();
            var model = svc.GetBulletinById(id);

            return View(model);
        }

        // EDIT -- Bulletin by id
        public ActionResult Edit(int id)
        {
            var service = CreateBulletinService();
            var detail = service.GetBulletinById(id);
            var model =
                new BulletinEdit
                {
                    BulletinId = detail.BulletinId,
                    BulletinTitle = detail.BulletinTitle,
                    BulletinText = detail.BulletinText,
                    ModifiedUtc = detail.ModifiedUtc
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BulletinEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BulletinId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBulletinService();

            if (service.UpdateBulletin(model))
            {
                TempData["SaveResult"] = "Bulletin was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bulletin could not be updated.");
            return View(model);
        }

        // DELETE -- Chore by id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBulletinService();
            var model = svc.GetBulletinById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBulletin(int id)
        {
            var service = CreateBulletinService();

            service.DeleteBulletin(id);

            TempData["SaveResult"] = "Bulletin was deleted";

            return RedirectToAction("Index");
        }

        // CreateBulletinService METHOD
        private BulletinService CreateBulletinService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BulletinService(userId);
            return service;
        }

    }
}