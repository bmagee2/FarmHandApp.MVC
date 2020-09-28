using FarmHandApp.Models;
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
        public ActionResult Create(ChoreCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

    }
}