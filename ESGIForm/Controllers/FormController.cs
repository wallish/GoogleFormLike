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
            if (Session["UserID"] == null) return RedirectToAction("Login", "Home");
            lstq = new List<Question>();
            Form form = new Form();
            
            using (var ctx = new Models.FormContext())
            {
                // form data
                form.FormId = Guid.NewGuid();
                form.CloseDate = DateTime.Now;
                form.DateInsert = DateTime.Now.ToString();
                form.DateUpdate = DateTime.Now.ToString();
                // add user
                Guid guid = new Guid(Session["UserID"].ToString());
                User user = ctx.Users.Where(u => u.UserId.Equals(guid)).FirstOrDefault();
                form.User = user;
                // generate hash
                Random random = new Random();
                var hash = random.Next();
                form.Hash = hash.ToString();

                ctx.Forms.Add(form);
                ctx.SaveChanges();
            }

            return View(form);
        }

        public ActionResult Edit()
        {
            //if (Session["UserID"] == null) return RedirectToAction("Login", "Home");
            List<Question> lstq = new List<Question>();
            string foo = "f5413e73-7c9c-4c82-9790-89d3dca2eba2";
            Guid guid = new Guid(foo);
            Form form;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                form = ctx.Forms.Where(f => f.FormId == guid).FirstOrDefault();
                lstq = new List<Question>(ctx.Questions.Where(q => q.FormId == guid)).OrderBy(q => q.DateInsert).ToList();
            }

            return View(form);
        }

        [HttpPost]
        public ActionResult Edit(Form formupdate)
        {
            /*List<Question> lstq = new List<Question>();
            string foo = "f5413e73-7c9c-4c82-9790-89d3dca2eba2";
            Guid guid = new Guid(foo);*/

            using (Models.FormContext ctx = new Models.FormContext())
            {
                Form form = ctx.Forms.Where(f => f.FormId == formupdate.FormId).FirstOrDefault();
                form.Description = formupdate.Description;
                form.Title = formupdate.Title;

                ctx.SaveChanges();
            }

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

        [HttpPost]
        public ActionResult Show()
        {
            List<Answer> list = new List<Answer>();
            Guid formid = Guid.NewGuid();
            Form form;
            foreach (string key in Request.Form.AllKeys)
            {
                if (key != "validate" && key !=  "formid")
                {
                    string[] value = Request.Form.GetValues(key);
                    string[] form_id = Request.Form.GetValues("formid");
                    Answer asw = new Answer();
                    asw.AnswerId = Guid.NewGuid();
                    Guid guid = new Guid(key);
                    asw.QuestionId = guid;
                    asw.Content = value[0];
                    formid = new Guid(form_id[0]);
                    asw.FormId = formid;
                    
                    list.Add(asw);
                }
                
            }

            using (Models.FormContext ctx = new Models.FormContext())
            {
                form = ctx.Forms.Where(f => f.FormId == formid).FirstOrDefault();
                foreach (Answer answer in list)
                {
                    ctx.Answers.Add(answer);
                    
                }
                ctx.SaveChanges();
            }


            return RedirectToAction("Summary","Form",form);
        }

        public ActionResult MyForms()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login","Home");
            List<Form> forms = new List<Form>() { };
            using (var ctx = new Models.FormContext())
            {
                Guid guid = new Guid(Session["UserID"].ToString());
                User user = ctx.Users.Where(u => u.UserId.Equals(guid)).FirstOrDefault();
                forms = ctx.Forms.Where(f => f.User.UserId == user.UserId).ToList();
                if (forms != null)
                {
                    return View(forms);
                }
            }
            return View();
        }

        public ActionResult Summary(Form form)
        {
            return View(form);
        }

    }
}
