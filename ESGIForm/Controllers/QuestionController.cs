using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESGIForm.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(Question question)
        {

            List<Question> foo;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                question.QuestionId = Guid.NewGuid();
                question.DateInsert = DateTime.Now;
                ctx.Questions.Add(question);
                ctx.SaveChanges();

                foo = new List<Question>(ctx.Questions.Where(q => q.FormId == question.FormId)).OrderBy(q => q.DateInsert).ToList();
            }

            return Json(foo, JsonRequestBehavior.AllowGet);
        }


        public bool Delete(Question question)
        {
            using (Models.FormContext ctx = new Models.FormContext())
            {
                Question qtoremove = ctx.Questions.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
                ctx.Questions.Remove(qtoremove);
                ctx.SaveChanges();

            }

            return true;
        }

        [HttpPut]
        public ActionResult Edit(Question question)
        {
            using (Models.FormContext ctx = new Models.FormContext())
            {
                Question qtoupdate = ctx.Questions.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
                qtoupdate.Title = question.Title;
                ctx.SaveChanges();

            }
            return View();

        }

    }
}
