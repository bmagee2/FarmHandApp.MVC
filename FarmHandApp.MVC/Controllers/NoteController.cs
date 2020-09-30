﻿using FarmHandApp.Models;
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
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var service = CreateNoteService();
            var model = service.GetAllNotes();

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
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        // GET DETAILS
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        // GET NOTES BY CHOREID?
        //public ActionResult ListOfNotesIndex(int id)
        //{
        //    var svc = CreateChoreService();
        //    var model = svc.GetNotesByChoreId(id);

        //    return View(model);
        //}

        // PUT
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    NoteText = detail.NoteText
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be updated.");
            return View(model);
        }


        // DELETE
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNote(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Note was deleted";

            return RedirectToAction("Index");
        }

        // CreateChoreService METHOD ??
        private ChoreService CreateChoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var recipeService = new ChoreService(userId);
            return recipeService;
        }

        // CreateNoteService METHOD
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}