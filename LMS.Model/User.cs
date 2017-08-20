using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace LMS.Models
{
    public class User :IdentityUser
    {
        

        public int age { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        public string address { get; set; }

        public bool gender { get; set; }

        public string picture { get; set; }

        public string lang { get; set; }
        public ICollection<Friend> Friends { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Rating> Ratings { get; set; }


    }
}