using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESGIForm.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage="Veuillez fournir un nom d'utilisateur",AllowEmptyStrings=false)]
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Veuillez fournir un nom d'utilisateur", AllowEmptyStrings = false)]
        public string Password { get; set; }


        public User GetUser(){
            Guid id = new Guid("097095ff-d590-48fd-b5cf-fc608ab6032e");
            User user = null;
            using (Models.FormContext ctx = new Models.FormContext())
            {
                user = ctx.Users.Find(id);
            }
         return user;
        }
    }

   
}