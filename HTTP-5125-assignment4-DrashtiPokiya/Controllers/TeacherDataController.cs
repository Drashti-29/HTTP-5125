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
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext Blog = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeacher(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            //Create an empty list of Teacher
            List<Teacher> teachers = new List<Teacher> { };
            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teachersId = Convert.ToInt32(ResultSet["teacherid"]);
                string teachersFname = ResultSet["teacherfname"].ToString();
                string teachersLname = ResultSet["teacherlname"].ToString();
                string employeeNumber = ResultSet["employeenumber"].ToString();
                Decimal teachersSalary = Convert.ToDecimal(ResultSet["salary"]);
                DateTime hireDate = Convert.ToDateTime(ResultSet["hiredate"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherId = teachersId;
                NewTeacher.teacherLname = teachersLname;
                NewTeacher.teacherFname = teachersFname;
                NewTeacher.employeeNumber = employeeNumber;
                NewTeacher.Salary = teachersSalary;

                //Add the teachers Name to the List
                teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return teachers;
        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
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
        /// Deletes an Teacher from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity.
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the author's table.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
     //  [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
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
    }
}
