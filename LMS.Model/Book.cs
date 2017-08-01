using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Book
    {
        [Key]
        public int ID { set; get; }
        public int ISBN { set; get; }
        public String title { set; get; }
    }
}