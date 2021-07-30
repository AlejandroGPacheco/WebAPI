using ItemData.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication2.Models;

namespace ItemData.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private IConfiguration Configuration;

        public ItemRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public void GetItems()
        {
            try
            {

                string ConnectionString = this.Configuration.GetConnectionString("MyConnection");

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

                    con.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public string PostItem(Item item)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    
                    using (SqlCommand command = new SqlCommand("InsertItem", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;

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
        public string PutItem([FromBody] Item item)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    
                    using (SqlCommand command = new SqlCommand("UpdateItem", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.NVarChar).Value = item.Id;
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = item.Name;
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = item.Description;


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

        public string DeleteItem(int i)
        {

            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    //string query = "DELETE FROM dbo.ItemDB WHERE [Id] = @id";
                    using (SqlCommand command = new SqlCommand("DeleteItem", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = i;
                        int rowDeleted = command.ExecuteNonQuery();
                        if (rowDeleted > 0)
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
