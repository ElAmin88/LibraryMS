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
    public class User_Group
    {
        [Key, Column(Order = 0)]
        public string User_Id { get; set; }
        [Key, Column(Order = 1)]
        public int Group_Id { get; set; }

        [ForeignKey("Group_Id")]
        public Group group { get; set; }
        [ForeignKey ("User_Id")]
        public User user { get; set; }
    }
}
