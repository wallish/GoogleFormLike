using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESGIForm.Models
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid FormId { get; set; }
        public string Content { get; set; }
    }
}