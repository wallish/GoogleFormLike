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
            /*using (var ctx = new Models.FormContext())
            {
                User user = new User();
                user.UserId = Guid.NewGuid();
                user.Username = "popolito";
                user.Nom = "popo";
                user.Prenom = "lito";
                ctx.Users.Add(user);
                ctx.SaveChanges();

            }*/


            return View();
        }

    

    }
}
