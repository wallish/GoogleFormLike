using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ESGIForm.Controllers
{
    public class StatController : Controller
    {
        //
        // GET: /Stat/

        public ActionResult Index()
        {

            return View();
        }

   


        public JsonResult Data(Guid guid)
        {
            List<Answer> answers = new List<Answer>();

            /*string bar = "c2ba17ff-4df5-46cb-b385-86e76ac2ebc7"; //debug
            Guid guid = new Guid(bar);*/
            Form form;
            List<Question> questions = new List<Question>();
            List<Answer> answer = new List<Answer>();
            using (var ctx = new Models.FormContext())
            {
                form = ctx.Forms.Where(q => q.FormId.Equals(guid)).FirstOrDefault();
                questions = ctx.Questions.Where(q => q.FormId.Equals(guid)).ToList();
                answer = ctx.Answers.Where(q => q.FormId.Equals(guid)).ToList();
                form.ListQuestion = questions;
            }

            var json = new JObject();
            foreach (Question item in questions)
            {
                int oui = answer.Where(a => a.QuestionId == item.QuestionId && a.Content == "oui").Count();
                int non = answer.Where(a => a.QuestionId == item.QuestionId && a.Content == "non").Count();

                JArray array = new JArray();
                array.Add(oui);
                array.Add(non);
                json[item.Title.ToString()] = array;
            }

            var foo = json.ToString();
            return Json(foo, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Show(Guid guid)
        {
            // security
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Summary", "Home");
               /* Form form;
                using (Models.FormContext ctx = new Models.FormContext())
                {
                    form = ctx.Forms.Where(f => f.FormId == guid).FirstOrDefault();
                    if (form.User.UserId != guid) {
                        return RedirectToAction("Summary", "Home");
                    }
                }*/
            }
           
            ViewData["guid"] = guid.ToString();
            return View();
        }
    }
}