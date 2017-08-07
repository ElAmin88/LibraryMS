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
        public string title { set; get; }
        public string details { set; get; }

        public ICollection<Reservation> Reservations { get; set; }

    }
}