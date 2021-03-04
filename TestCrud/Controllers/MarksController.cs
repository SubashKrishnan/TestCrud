using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestCrud.Model;

namespace TestCrud.Controllers
{
    [Route("api/[controller]")]
    public class MarksController : Controller
    {
        [HttpGet("GetMarks")]
        public IActionResult GetMarks()
        {
            List<MarksModel> marks = new List<MarksModel>(); //list
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetMarks", connection);

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            MarksModel mark = new MarksModel(); // model name(same table coloumn)
                            mark.STUDENT_ID = new Guid(dataReader["STUDENT_ID"].ToString());
                            mark.M1 = Convert.ToInt32(dataReader["M1"]);
                            mark.M2 = Convert.ToInt32(dataReader["M2"]);
                            mark.M3 = Convert.ToInt32(dataReader["M3"]);
                            mark.DATE = Convert.ToDateTime(dataReader["DATE"]);
                            marks.Add(mark); //add each row to list
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(marks);
        }
        
        [HttpPost("InsertMark")]
        public IActionResult InsertMark([FromBody] MarksModel marks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Insertmark", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@student_id", marks.STUDENT_ID);
                        cmd.Parameters.AddWithValue("@m1", marks.M1);
                        cmd.Parameters.AddWithValue("@m2", marks.M2);
                        cmd.Parameters.AddWithValue("@m3", marks.M3);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("DeleteMark")]
        public IActionResult DeleteMark([FromBody] MarksModel marks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Deletemark", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@student_id", marks.STUDENT_ID);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return Ok("Deleted successfully");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("UpdateMark")]
        public IActionResult UpdateMark([FromBody] MarksModel mark)
        {
            try
            {
                 using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateMark", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        cmd.Parameters.AddWithValue("@m1", mark.M1);
                        cmd.Parameters.AddWithValue("@m2", mark.M2);
                        cmd.Parameters.AddWithValue("@m3", mark.M3);
                        cmd.Parameters.AddWithValue("@student_id", mark.STUDENT_ID);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return Ok("updated successfully");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
