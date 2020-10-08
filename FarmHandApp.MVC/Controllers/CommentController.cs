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
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            var service = CreateCommentService();
            var model = service.GetAllComments();

            return View(model);
        }

        //// GET -- Create
        //public ActionResult Create()
        //{
        //    return View();
        //}



        //// POST -- Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CommentCreate model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var service = CreateCommentService();

        //    if (service.CreateComment(model))
        //    {
        //        TempData["SaveResult"] = "Comment was created.";
        //        return RedirectToAction("Index");
        //    };

        //    ModelState.AddModelError("", "Comment could not be created.");

        //    return View(model);
        //}

        //// CREATE COMMENT WITH BULLETINID
        public ActionResult CreateCommentWithBulletinId(int id)
        {
            var service = CreateCommentService();
            var detail = service.GetBulletinById(id);

            var model =
                new CommentCreate
                {
                    BulletinId = detail.BulletinId
                };

            return View(model);
        }

        //// WITH VIEWBAG
        //public ActionResult CreateCommentWithBulletinId(int id)
        //{
        //    var service = CreateBulletinService();
        //    ViewBag.BulletinDetail = service.GetBulletinById(id);

        //    var model =
        //        new CommentCreate
        //        {
        //            BulletinId = id
        //        };

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCommentWithBulletinId(CommentCreate model)    // MATCH THE METHOD NAMES!!
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCommentService();

            if (service.CreateBulletinComment(model))
            {
                TempData["SaveResult"] = "Note was created.";
                return RedirectToAction("ListOfCommentsForBulletin", new { id = model.BulletinId });
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        // GET ALL COMMENTS FOR ONE BULLETIN
        public ActionResult ListOfCommentsForBulletin(int id)
        {
            var service = CreateCommentService();
            var model = service.GetCommentsByBulletinId(id);
            return View(model);
        }


        // GET DETAILS
        public ActionResult Details(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);

            return View(model);
        }

        // PUT
        public ActionResult Edit(int id)
        {
            var service = CreateCommentService();
            var detail = service.GetCommentById(id);
            var model =
                new CommentEdit
                {
                    CommentId = detail.CommentId,
                    CommentText = detail.CommentText
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CommentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCommentService();

            if (service.UpdateComment(model))
            {
                TempData["SaveResult"] = "Comment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Comment could not be updated.");
            return View(model);
        }


        // DELETE
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            var service = CreateCommentService();

            service.DeleteComment(id);

            TempData["SaveResult"] = "Comment was deleted";

            return RedirectToAction("Index");
        }

        // CreateCommentService METHOD
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;
        }
    }
}