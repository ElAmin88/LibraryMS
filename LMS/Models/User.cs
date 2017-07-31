using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class User
    {
        [Key]
        public int ID { set; get; }
        [MinLength(5)]
        public string name { set; get; }
        [MinLength(8)]
        public string password { set; get; }
    }
}