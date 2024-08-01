using Assignment3.Models;
using Mysqlx.Datatypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        // This action method handles requests to the base URL for the Teacher controller.
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        // This action method handles requests to the /Teacher/List URL.
        // It optionally accepts a SearchKey parameter to filter the list of teachers.
        public ActionResult List(string SearchKey = null)
        {
            // Create an instance of the TeacherDataController to access its methods.
            TeacherDataController controller = new TeacherDataController();

            // Call the ListTeacher method of TeacherDataController to get the list of teachers.
            // Pass the SearchKey to filter the results.
            IEnumerable<Teacher> teacher = controller.ListTeacher(SearchKey);

            // Return the View with the list of teachers.
            return View(teacher);
        }

        // GET: /Teacher/Show
        // This action method handles requests to the /Teacher/Show URL.
        // It accepts an id parameter to retrieve details of a specific teacher.
        public ActionResult Show(int id)
        {
            // Create an instance of the TeacherDataController to access its methods.
            TeacherDataController controller = new TeacherDataController();

            // Call the FindTeacher method of TeacherDataController to get details of the specified teacher.
            Teacher NewTeacher = controller.FindTeacher(id);

            // Return the View with the details of the teacher.
            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}
        [System.Web.Http.HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        //POST : /Teacher/Create
        [System.Web.Http.HttpPost]
        public ActionResult Create(int teacherid, string TeacherFname, string TeacherLname, decimal salary, string employeeNumber, DateTime hireDate)
        {     
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherid);
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(salary);
            Debug.WriteLine(employeeNumber);
            Debug.WriteLine(hireDate);

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherId = teacherid;
            NewTeacher.teacherFname = TeacherFname;
            NewTeacher.teacherLname = TeacherLname;
            NewTeacher.Salary = salary;
            NewTeacher.employeeNumber = employeeNumber;
            NewTeacher.HireDate = hireDate;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}
