using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestCrud.Model;

namespace TestCrud.Helpers
{
    public class Sqlconnnection
    {
        public void Insertstudent(string SP, string name, int age)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand(SP, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@age", age);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                //return Ok("inserted successfully");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Ok(string v)
        {
            throw new NotImplementedException();
        }
    }
}
