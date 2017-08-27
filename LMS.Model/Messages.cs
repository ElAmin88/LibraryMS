using LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Messages
    {
        [Key]
        public int Id { set; get; }

        public string from { set; get; }
        public int group_Id { set; get; }
        
        public string msg { set; get; }

        [ForeignKey("group_Id")]
        public Group g { set; get; }

        [ForeignKey("from")]
        public User user {set;get;}

    }
}
