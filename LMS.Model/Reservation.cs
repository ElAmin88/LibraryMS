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
        [Required]
        [Key, Column(Order = 0)]
        public int userID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int bookID { get; set; }

    }
}
