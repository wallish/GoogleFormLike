using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESGIForm.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User u)
        {
            using (var ctx = new Models.FormContext())
            {
                u.UserId = Guid.NewGuid();
                ctx.Users.Add(u);
                ctx.SaveChanges();

                return RedirectToAction("Login");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User usr)
        {

            using (var ctx = new Models.FormContext())
            {
                var user = ctx.Users.Where(u => u.Username.Equals(usr.Username) && u.Password.Equals(usr.Password)).FirstOrDefault();
                if (user != null)
                {
                    Session["UserID"] = user.UserId.ToString();
                    Session["UserName"] = user.Username.ToString();
                    return RedirectToAction("Panel");
                }
            
            
            }

            return View();
        }

        public ActionResult Panel()
        {
            if (Session["UserID"] != null)
            {
                List<Form> forms = new List<Form>();
                using (var ctx = new Models.FormContext())
                {
                    Guid guid = new Guid(Session["UserID"].ToString());
                    User user = ctx.Users.Where(u => u.UserId == guid).FirstOrDefault();
                    forms = ctx.Forms.Where(f => f.User.UserId == user.UserId).OrderByDescending(f => f.DateInsert).ToList();
                }

                return View(forms);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Disonnect()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Home");
            Session["UserId"] = null;
            Session["Username"] = null;

            return RedirectToAction("Login");
        }

    

    }
}
