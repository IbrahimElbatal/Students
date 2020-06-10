using StudentTask.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace StudentTask.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentEntities db;
        public StudentController()
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
            var students = db.Students
                .Include(s => s.Field)
                .Include(s => s.Neighborhood)
                .Include(s => s.Governorate)
                .Include(st => st.StudentTeachers.Select(s => s.Teacher))
                .ToList();

            return View(students);
        }

        public ActionResult SaveStudent()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Fields = new SelectList(db.Fields.ToList(), "ID", "Name");
            ViewBag.Governorates = new SelectList(db.Governorates.ToList(), "ID", "Name");
            //            ViewBag.Neighborhoods = new SelectList(db.Neighborhoods.ToList(), "ID", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            db.Students.Add(student);
            db.SaveChanges();

            return RedirectToAction("SaveStudent");
        }

        [HttpGet]
        public ActionResult GetNeighborhood()
        {
            return Json(db.Neighborhoods.Select(n => new
            {
                n.ID,
                n.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.ID == id);
            if (student == null)
                return HttpNotFound($"the student with Id {id} not found");

            ViewBag.Fields = new SelectList(db.Fields.ToList(), "ID", "Name", student.FieldId);
            ViewBag.Governorates = new SelectList(db.Governorates.ToList(), "ID", "Name", student.GovernorateId);
            ViewBag.Neighborhoods = new SelectList(db.Neighborhoods.ToList(), "ID", "Name", student.NeighborhoodId);


            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var studentInDb = db.Students.FirstOrDefault(s => s.ID == student.ID);
            if (studentInDb == null)
                return HttpNotFound($"the student with Id {student.ID} not found");

            studentInDb.Name = student.Name;
            studentInDb.BirthDate = student.BirthDate;
            studentInDb.FieldId = student.FieldId;
            studentInDb.GovernorateId = student.GovernorateId;
            studentInDb.NeighborhoodId = student.NeighborhoodId;

            db.SaveChanges();

            return View("SaveStudent");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.ID == id);
            if (student == null)
                return HttpNotFound($"the student with Id {id} not found.");
            if (student.StudentTeachers.Any())
            {
                //this code for when delete student not delete because it is attatched to teacher
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                //"Not Allow To delete This Student Because of it is attached to a teacher.");

                // this code delete student although it is attatched to teacher
                db.StudentTeachers.RemoveRange(student.StudentTeachers);
            }

            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}