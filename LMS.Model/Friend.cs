using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Friend
    {
        
        [Required]
        [Key, Column(Order = 0)]
        public int user1ID { get; set;}
        [Required]
        [Key, Column(Order = 1)]
        public int user2ID { get; set; }
    }
}
