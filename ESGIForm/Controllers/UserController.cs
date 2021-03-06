﻿using ESGIForm.Models;
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
        public ActionResult Add()
        {
            //User user = new User();
            return View(new User());
        }

        [HttpPost]
        public ActionResult Add(User user)
        {
            using (var ctx = new Models.FormContext())
            {
                user.UserId = Guid.NewGuid();
                ctx.Users.Add(user);
                ctx.SaveChanges();

                return RedirectToAction("Show", user);
            }

        }

        public ActionResult Show(User user){

            return View(user);
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

        public User GetUser(Guid id)
        {
            User user = null;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                user = ctx.Users.Find(id);
            }

            return user;
        }

        /* check user un database */

        public bool CheckUsername(string username)
        {
            using (var ctx = new Models.FormContext())
            {

                User user = ctx.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
                if (user != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
