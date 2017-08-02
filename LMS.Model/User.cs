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

        public int age { set; get; }

        public string type { set; get; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        public List<User> friends { get; set; }
    }
}