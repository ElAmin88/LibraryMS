using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public int rating { get; set; }
        public int bookID { get; set; }
        public string userID { get; set; }

        public string review { get; set; }

        [ForeignKey("userID")]
        public User user{ get; set; }
        [ForeignKey("bookID")]
        public Book book { get; set; }
    }
}
