using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    /// <summary>
    /// The TeacherController class manages the web-based interactions with teacher data, 
    /// including listing, viewing, creating, updating, and deleting teachers.
    /// </summary>
    public class TeacherController : Controller
    {
        /// <summary>
        /// Handles requests to the base URL for the Teacher controller.
        /// </summary>
        /// <returns>View for the index page.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Routes to the page for creating a new teacher.
        /// </summary>
        /// <returns>View for creating a new teacher.</returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Routes to the page for creating a new teacher using AJAX.
        /// </summary>
        /// <returns>View for creating a new teacher with AJAX.</returns>
        public ActionResult Ajax_New()
        {
            return View();
        }

        /// <summary>
        /// Displays a list of teachers, optionally filtered by a search key.
        /// </summary>
        /// <param name="SearchKey">Optional search key to filter the list of teachers.</param>
        /// <returns>View with the list of teachers.</returns>
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teacher = controller.ListTeacher(SearchKey);
            return View(teacher);
        }

        /// <summary>
        /// Displays the details of a specific teacher based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to display.</param>
        /// <returns>View with the details of the teacher.</returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        /// <summary>
        /// Routes to a confirmation page before deleting a teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted.</param>
        /// <returns>View with the teacher's details for confirmation.</returns>
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        /// <summary>
        /// Deletes a specific teacher based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted.</param>
        /// <returns>Redirects to the list of teachers after deletion.</returns>
        [System.Web.Http.HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Creates a new teacher with the provided details.
        /// </summary>
        /// <param name="teacherid">The ID of the teacher.</param>
        /// <param name="TeacherFname">The first name of the teacher.</param>
        /// <param name="TeacherLname">The last name of the teacher.</param>
        /// <param name="salary">The salary of the teacher.</param>
        /// <param name="employeeNumber">The employee number of the teacher.</param>
        /// <param name="hireDate">The hire date of the teacher.</param>
        /// <returns>Redirects to the list of teachers after creation.</returns>
        [System.Web.Http.HttpPost]
        public ActionResult Create(int teacherid, string TeacherFname, string TeacherLname, decimal salary, string employeeNumber, DateTime hireDate)
        {
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherid);
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(salary);
            Debug.WriteLine(employeeNumber);
            Debug.WriteLine(hireDate);

            Teacher NewTeacher = new Teacher
            {
                teacherId = teacherid,
                teacherFname = TeacherFname,
                teacherLname = TeacherLname,
                Salary = salary,
                employeeNumber = employeeNumber,
                HireDate = hireDate
            };

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to the Update confirmation page, gathering current information from the database.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <returns>View for confirming update details.</returns>
        public ActionResult UpdateConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Updates a specific teacher's information based on the provided details.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <param name="TeacherFname">The updated first name of the teacher.</param>
        /// <param name="TeacherLname">The updated last name of the teacher.</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <param name="Salary">The updated salary of the teacher.</param>
        /// <returns>Redirects to the teacher's detail page after updating.</returns>
        [System.Web.Http.HttpPost]
        public ActionResult Update(int id, string TeacherFname, DateTime hiredate, string TeacherLname, string EmployeeNumber, decimal Salary)
        {
            try
            {
                Teacher TeacherInfo = new Teacher
                {
                    teacherFname = TeacherFname,
                    teacherLname = TeacherLname,
                    employeeNumber = EmployeeNumber,
                    Salary = Salary,
                    HireDate = hiredate
                };

                TeacherDataController controller = new TeacherDataController();
                controller.UpdateTeacher(id, TeacherInfo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Teacher");
            }

            return RedirectToAction("Show/" + id);
        }
    }
}
