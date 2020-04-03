using FantasticBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasticBookStore.Controllers
{
    public class UserController : Controller
    {
        FantasticContext BookDb = new FantasticContext();
        // GET: User
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(user users)
        {


            if (ModelState.IsValid)
            {
                BookDb.users.Add(users);
                BookDb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(users);

        }
    }
}