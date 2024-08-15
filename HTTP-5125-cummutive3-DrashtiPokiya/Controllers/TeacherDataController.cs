using System;
using System.Collections.Generic;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext Blog = new SchoolDbContext();

        /// <summary>
        /// Retrieves a list of teachers from the database based on a search key.
        /// The search key can be a part of the teacher's first name, last name, or both.
        /// </summary>
        /// <param name="SearchKey">An optional search key to filter the teachers.</param>
        /// <returns>A list of teachers matching the search criteria.</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeacher(string SearchKey = null)
        {
            MySqlConnection Conn = Blog.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();

            while (ResultSet.Read())
            {
                int teachersId = Convert.ToInt32(ResultSet["teacherid"]);
                string teachersFname = ResultSet["teacherfname"].ToString();
                string teachersLname = ResultSet["teacherlname"].ToString();
                string employeeNumber = ResultSet["employeenumber"].ToString();
                Decimal teachersSalary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher
                {
                    teacherId = teachersId,
                    teacherLname = teachersLname,
                    teacherFname = teachersFname,
                    employeeNumber = employeeNumber,
                    Salary = teachersSalary
                };

                teachers.Add(NewTeacher);
            }

            Conn.Close();
            return teachers;
        }

        /// <summary>
        /// Finds a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to be found.</param>
        /// <returns>The teacher object corresponding to the provided ID.</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            MySqlConnection Conn = Blog.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teachersId = Convert.ToInt32(ResultSet["teacherid"]);
                string teachersFname = ResultSet["teacherfname"].ToString();
                string teachersLname = ResultSet["teacherlname"].ToString();
                string employeeNumber = ResultSet["employeenumber"].ToString();
                DateTime hireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                Decimal teachersSalary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.teacherId = teachersId;
                NewTeacher.teacherLname = teachersLname;
                NewTeacher.teacherFname = teachersFname;
                NewTeacher.Salary = teachersSalary;
                NewTeacher.employeeNumber = employeeNumber;
                NewTeacher.HireDate = hireDate;
            }

            return NewTeacher;
        }

        /// <summary>
        /// Deletes a teacher from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted.</param>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = Blog.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        /// <summary>
        /// Adds a new teacher to the database.
        /// </summary>
        /// <param name="NewTeacher">The teacher object containing information to be added to the database.</param>
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            MySqlConnection Conn = Blog.AccessDatabase();
            Debug.WriteLine(NewTeacher.teacherFname);
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "values (@teacherid,@teacherfname,@teacherlname, @employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherid", NewTeacher.teacherId);
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        /// <summary>
        /// Updates the details of an existing teacher in the database.
        /// </summary>
        /// <param name="id">The ID of the teacher to be updated.</param>
        /// <param name="TeacherInfo">The teacher object containing updated information.</param>
        [HttpPost]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            MySqlConnection Conn = Blog.AccessDatabase();

            try
            {
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "UPDATE teachers SET teacherfname=@TeacherFname, teacherlname=@TeacherLname, hiredate=@hiredate, employeenumber=@EmployeeNumber, salary=@Salary WHERE teacherid=@TeacherId";
                cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.teacherFname);
                cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.teacherLname);
                cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.employeeNumber);
                cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
                cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.HireDate);
                cmd.Parameters.AddWithValue("@TeacherId", id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
                throw new ApplicationException("Issue was a database issue.", ex);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw new ApplicationException("There was a server issue.", ex);
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}
