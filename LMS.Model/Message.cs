using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Message
    {
        [Key, Column(Order = 0)]
        public string user1ID { get; set; }

        [Key, Column(Order = 1)]
        public string user2ID { get; set; }

        public string message { get; set; }

        [ForeignKey("user1ID")]
        public User user1 { get; set; }

        [ForeignKey("user2ID")]
        public User user2 { get; set; }
    }
}
