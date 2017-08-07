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
        
        [Key,Column(Order = 0)]
        public int user1ID { get; set;}
        
        [Key,Column(Order = 1)]
        public int user2ID { get; set; }
        
        [ForeignKey("user1ID")]
        public User user1 { get; set; }
        [ForeignKey("user2ID")]
        public User user2 { get; set; }
    }
}
