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
                //form.User = user;
                var foo = random.Next();
                form.Hash = foo.ToString();
                ctx.Forms.Add(form);
                ctx.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        public JsonResult Add(Question question)
        {
            /*using (Models.FormContext ctx = new Models.FormContext())
            {
                question.QuestionId = Guid.NewGuid();
                ctx.Questions.Add(question);
                ctx.SaveChanges();
            }*/

            if (!string.IsNullOrEmpty(Request.Form["close"]))
            {
               
            }
            question.QuestionId = Guid.NewGuid();

            
            lstq.Add(question);
            return Json(lstq, JsonRequestBehavior.AllowGet);
        }

        public JsonResult List()
        {
            //lstq = new List<Question>();


            /*Question question = new Question() { Title = "zaza", Description = "toto" };
            question.QuestionId = Guid.NewGuid();

            lstq.Add(question);*/

            return Json(lstq, JsonRequestBehavior.AllowGet);
        }

       

        public bool Delete(Question question)
        {
            Question q2 = lstq.Where(q => q.QuestionId == question.QuestionId).FirstOrDefault();
            if (q2 != null)
            {
                lstq.Remove(q2);
            }
            return true;
        }

        [HttpPut]
        public ActionResult Edit(Question question)
        {
            foreach(var q in lstq){
                if(q.QuestionId == question.QuestionId)
                {
                    q.Title = question.Title;
                }
            }

            var toto = lstq;
            return View();

        }

      


    }
}
