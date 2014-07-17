using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using ESGIForm.Models;
using System.Text;

namespace ESGIForm.Controllers
{
    public class PdfController : Controller
    {
        //
        // GET: /Pdf/

        public ActionResult Index()
        {
            System.IO.FileStream fs = new FileStream(Server.MapPath("pdf") + "\\" + "First PDF document.pdf", FileMode.Create);

            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF
            // Writer class using the document and the filestrem in the constructor.

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

           
            // Add meta information to the document

            document.AddAuthor("Form online");
            document.AddCreator("Form online");
            document.AddKeywords("Form");
            document.AddSubject("Statistique formulaire");
            document.AddTitle("Statistique formulaire");

            // Open the document to enable you to write to the document
            document.Open();
            // Add a simple and wellknown phrase to the document in a flow layout manner
            document.Add(new Paragraph("Hello World!"));
            // Close the document
            document.Close();
            // Close the writer instance
            writer.Close();
            // Always close open filehandles explicity
            fs.Close();

            
            return View();
        }

        public String Data(Guid guid)
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

            return json.ToString();
        }


        public ActionResult Export(Guid guid)
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Home");
            /*var client = new WebClient();
            client.Headers.Add("User-Agent", "Nobody");
            var response = client.DownloadString(new Uri("http://localhost:60164/Stat/Data?guid=54ab59e0-b1e9-474d-9cd8-95427d178646"));
            */
            //String o = this.Data(new Guid("54ab59e0-b1e9-474d-9cd8-95427d178646"));
            String o = this.Data(guid);
            JObject root = JObject.Parse(o);

            StringBuilder sb = new StringBuilder();
           
            sb.Append("<h2>Exportation des statistiques</h2>");
            sb.Append("<table><tr><td>Question</td><td>Réponse oui</td><td>Réponse non</td></tr>");

            foreach (var item in root)
            {
                sb.Append("<tr><td>" + item.Key + "</td><td>" + item.Value.First + "</td><td>" + item.Value.Last + "</td></tr>");

            }
            sb.Append("</table>");
            sb.Append(DateTime.Now.ToString());
            String strResult = sb.ToString();

            using (MemoryStream ms = new MemoryStream())
            {

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                iTextSharp.text.html.simpleparser.HTMLWorker hw =
                new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                hw.Parse(new StringReader(strResult));
                document.Close();
                writer.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition",
                "attachment;filename=export.pdf");
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

            }
            return View();
        }
    }
}
