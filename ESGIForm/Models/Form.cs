using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESGIForm.Models
{
    public class Form
    {
        public Guid FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CloseDate { get; set; }
        public string Hash { get; set; }
        public string Status { get; set; }
        public string DateInsert { get; set; }
        public string DateUpdate { get; set; }
        public User User { get; set; }
        public  ICollection<Question> ListQuestion { get; set; }

       

    }
}