using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESGIForm.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Guid FormId { get; set; }
    }
}