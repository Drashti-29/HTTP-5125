using Assignment3.Models;
using System;
using System.Collections.Generic;
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
    }
}
