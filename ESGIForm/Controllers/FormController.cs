using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESGIForm.Controllers
{
    public class FormController : Controller
    {
        //
        // GET: /Form/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Add(Form form)
        {


            return View();
        }

    }
}
