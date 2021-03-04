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
using System.Net;
using System.Net.Mail;
using TestCrud.Helpers;

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
                Sqlconnnection sq = new Sqlconnnection();
                sq.Insertstudent("SP", student.NAME, student.AGE);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok("inserted successfully");
        }

        [HttpPost("DeleteStudent")]
        public IActionResult DeleteSudent([FromBody] StudentsModel student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Deletestudent", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@age", student.AGE);
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

        [HttpPost("UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] StudentsModel student)
        {
            try
            {
                TestModel g = new TestModel();
                g.Test1();

                
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateStudent", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", student.NAME);
                        cmd.Parameters.AddWithValue("@age", student.AGE);
                        cmd.Parameters.AddWithValue("@id", student.ID);
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

        [HttpGet("GetStudentsMarks")]//methoad name or action
        public IActionResult GetStudentsMarks()
        {

            List<StudentMark> SMlist = new List<StudentMark>(); //list
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetStudentsMarks", connection);
                  

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                          StudentMark sm = new StudentMark(); // model name(same table coloumn)
                            sm.ID = new Guid(dataReader["ID"].ToString());
                            sm.NAME= Convert.ToString(dataReader["NAME"]);
                            sm.AGE= Convert.ToInt32(dataReader["AGE"]);
                            sm.DATE = Convert.ToDateTime(dataReader["DATE"]);
                            sm.M1 = dataReader["MARK1"] as int? ?? default(int);
                            sm.M2 = dataReader["MARK2"] as int? ?? default(int);
                            sm.M3 = dataReader["MARK3"] as int? ?? default(int);
                            SMlist.Add(sm);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(SMlist); //return the list
        }

        [HttpPost("GetOneStudentsMarks")]//methoad name or action
        public IActionResult GetOneStudentsMarks([FromBody] GetValforOneStudentmark GetVal)
        {

            List<OneStudentMark> SMlist = new List<OneStudentMark>(); //list
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=dev;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=admin@123"))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_GetOneStudentsMarks", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", GetVal.ID);
                        //cmd.ExecuteReader();
                        //connection.Close();
                    

                    //connection.Open();
                    //SqlCommand command = new SqlCommand("SP_GetOneStudentsMarks", connection);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    

                        while (dataReader.Read())
                        {
                            OneStudentMark Osm = new OneStudentMark(); // model name(same table coloumn)
                            Osm.ID = new Guid(dataReader["ID"].ToString());
                            Osm.NAME = Convert.ToString(dataReader["NAME"]);
                            Osm.AGE = Convert.ToInt32(dataReader["AGE"]);
                            Osm.DATE = Convert.ToDateTime(dataReader["DATE"]);
                            Osm.M1 = Convert.ToInt32(dataReader["MARK1"] ?? "0");
                            Osm.M2 = Convert.ToInt32(dataReader["MARK2"] ?? "0");
                            Osm.M3 = Convert.ToInt32(dataReader["MARK3"] ?? "0");
                            SMlist.Add(Osm); //add each row to list
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(SMlist); //return the list
        }
    }
}
