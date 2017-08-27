using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Data;

namespace LMS.Models
{
    public static class ResTest
    {
        public static DataTable getUsers(string id)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\LOCALDB;Initial Catalog=LMS;Integrated Security=True");
            con.Open();
            //SqlCommand com = new SqlCommand("SELECT * FROM AspNetUsers Where Id= '"+id+"'",con);
            SqlCommand com = new SqlCommand("GetUser",con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("Param1",id);
            /*var x= com.ExecuteReader();
            while (x.Read())
            {

            }*/
            SqlDataAdapter sda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            

            con.Close();

            return dt;
            
        }

        public static List<AspNetUser> GetAllUsers()
        {
            LinqTestDataContext l = new LinqTestDataContext();
             
            return l.AspNetUsers.Where(x=> x.UserName =="user1").ToList();
        }

        public static string AddHello (this string s)
        {
            return  "Hello"+s;
        }
    }
}