using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESGIForm.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        //affiche le formulaire
        public ActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddUser(User user)
        {
            using (var ctx = new Models.FormContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();

                return RedirectToAction("Detail", user);
            }

        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> res = null;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                res = ctx.Users.ToList();
            }
            return res;
        }

        [HttpGet]
        public User GetUser(Guid id)
        {
            User user = null;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                user = ctx.Users.Find(id);
            }

            return user;
        }

    }
}
