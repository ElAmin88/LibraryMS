using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Reservation
    {
        [Key, Column(Order = 0)]
        public string userID { get; set; }

        [Key, Column(Order = 1)]
        public int bookID { get; set; }

        public int status { get; set; } // 0=>waiting approval , 1=>reserved , 2=>waiting for retrieval 

        [ForeignKey("userID")]
        public User user { get; set; }
        [ForeignKey("bookID")]
        public Book book { get; set; }
    }
}
