﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESGIForm.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
    }
}