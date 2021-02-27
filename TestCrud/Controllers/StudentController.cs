using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestCrud.Model;

namespace TestCrud.Controllers
{
    [Route("api/[controller]")] // declar route
    public class StudentController : Controller
    {
        [HttpGet("GetStudents")]//methoad name or action
        public IActionResult GetStudents()
        {

            List<StudentsModel> students = new List<StudentsModel>(); //list
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetStudents", connection);
                  
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            StudentsModel student = new StudentsModel(); // model name(same table coloumn)
                            student.ID = new Guid(dataReader["ID"].ToString());
                            student.NAME = Convert.ToString(dataReader["NAME"]);
                            student.AGE = Convert.ToInt32(dataReader["AGE"]);
                            student.DATE = Convert.ToDateTime(dataReader["DATE"]);
                            students.Add(student); //add each row to list
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(students); //return the list
        }

        [HttpPost("InsertStudent")]
        public IActionResult InsertSudent([FromBody] StudentsModel student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_InsertStudent", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", student.NAME);
                        cmd.Parameters.AddWithValue("@age", student.AGE);
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
    }
}
