using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : Controller
    {
        string ConnectionString = "Data Source=A320LLHR\\SQLEXPRESS; Initial Catalog=practiceDB; Integrated Security=SSPI";
        public List<Item> list = new List<Item>()
        {
            new Item {Id = 1, Name = "Footbal", isComplete = false },
            new Item {Id = 2, Name = "Basketball", isComplete = true }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAllItems()
        {
            return list;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            try
            {

                //OleDbConnection connection = new OleDbConnection();
                //SqlConnection cnn;
                //string query = "SELECT * FROM dbo.ItemDB;";
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SelectAllItem", con);
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0} {1} {2}", reader[0], reader[1], reader[2]));
                        }
                    }
                    //Console.WriteLine("Successful");
                    con.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        [HttpPost]
        public string PostItem(Item item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string query = "INSERT INTO dbo.ItemDB ([Id], [Name], [description]) VALUES (@Id, @Name, @Description);";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add("@id", SqlDbType.NVarChar).Value = item.Id;
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = item.Name;
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = item.isComplete;

                        int rowAdded = command.ExecuteNonQuery();
                        if (rowAdded > 0)
                        {
                            return "It worked";
                        }
                        
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Not worked";

        }
    }
}
