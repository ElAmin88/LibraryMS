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
        [Required(ErrorMessage = "Please Enter Name more than 5 Characters")]
        public string name { set; get; }
        [MinLength(8)]
        [Required(ErrorMessage = "Please Enter Name more than 8 characters")]
        public string password { set; get; }
    }
}