using System;
using StudentTask.Models;
using System.Linq;
using System.Web.Mvc;

namespace StudentTask.Controllers
{
    public class TeacherController : Controller
    {
        private readonly StudentEntities db;
        public TeacherController()
        {
            db = new StudentEntities();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var teachers = db.Teachers.ToList();

            return View(teachers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
                return View(teacher);

            teacher.ID = !db.Teachers.Any() ? 1: db.Teachers.Max(t => t.ID) + 1;
            db.Teachers.Add(teacher);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}