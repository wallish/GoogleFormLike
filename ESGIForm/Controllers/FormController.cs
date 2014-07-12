using ESGIForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Data.Entity;

namespace ESGIForm.Controllers
{
    public class FormController : Controller
    {
        // GET: /Form/
        
        private static List<Question> lstq = new List<Question>();
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Add()
        {

            lstq = new List<Question>();
            Form form = new Form();
            
            using (var ctx = new Models.FormContext())
            {
                Random random = new Random();
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                form.FormId = Guid.NewGuid();
                form.CloseDate = DateTime.Now;
                form.DateInsert = DateTime.Now.ToString();
                form.DateUpdate = DateTime.Now.ToString();
                //form.User = user;
                var foo = random.Next();
                form.Hash = foo.ToString();
                ctx.Forms.Add(form);
                ctx.SaveChanges();
            }

            return View(form);
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

        public JsonResult List(Guid guid)
        {
            List<Question> foo;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                foo = new List<Question>(ctx.Questions.Where(q => q.FormId == guid)).OrderBy(q => q.DateInsert).ToList();
            }

            return Json(foo, JsonRequestBehavior.AllowGet);
        }

       

        public bool Delete(Question question)
        {
            /*Question q2 = lstq.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
            if (q2 != null)
            {
                lstq.Remove(q2);
            }*/
            //List<Question> foo;
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
            /*foreach(var q in lstq){
                if(q.QuestionId == question.QuestionId)
                {
                    q.Title = question.Title;
                }
            }

            var toto = lstq;*/

            using (Models.FormContext ctx = new Models.FormContext())
            {
                Question qtoupdate = ctx.Questions.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
                qtoupdate.Title = question.Title;
                ctx.SaveChanges();

            }
            return View();

        }

        [HttpPost]
        public ActionResult Close(Guid guid)
        {
            Form form;
            using (var ctx = new Models.FormContext())
            {
                 form = ctx.Forms.Where(f => f.FormId == guid).FirstOrDefault() ;
                 if (form != null)
                 {
                     form.DateUpdate = DateTime.Now.ToString();
                     form.Status = "1";
                     ctx.SaveChanges();
                 }
            }

            return RedirectToAction("Share", "Form", form);
        }

        public ActionResult Share(Form guid)
        {
            return View(guid);
        }

        public ActionResult Show(string hash)
        {
            Form form;
            List<Question> listquestions = new List<Question>() { };
            using (var ctx = new Models.FormContext())
            {
                form = ctx.Forms.Where(f => f.Hash == hash).FirstOrDefault();
                if (form != null)
                {
                    listquestions = ctx.Questions.Where(q => q.FormId == form.FormId).OrderBy(q => q.DateInsert).ToList();
                }
            }

            //form.setList(listquestions);
            form.ListQuestion = listquestions;

            return View(form);
        }
      


    }
}
