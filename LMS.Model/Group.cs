using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Group
    {
        [Key]
        public int Id { set; get; }
        public string name { set; get; }

        public string last_Message { set; get; }
        public ICollection<User_Group> User_Group { get; set; }
        public ICollection<Messages> Messages { get; set; }


    }
}
