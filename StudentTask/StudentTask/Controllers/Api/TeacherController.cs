using StudentTask.Dtos;
using StudentTask.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StudentTask.Controllers.Api
{
    public class TeacherController : ApiController
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
        [HttpPost]
        public IHttpActionResult Create(TeacherDto dto)
        {
            var teacherId = !db.Teachers.Any() ? 1 : db.Teachers.Max(t => t.ID) + 1;
            var teacher = new Teacher()
            {
                ID = teacherId,
                Name = dto.Name,
                BirthDate = dto.BirthDate
            };

            var studentTeachers = new List<StudentTeacher>();
            foreach (var studentId in dto.StudentIds)
            {
                studentTeachers.Add(new StudentTeacher()
                {
                    TeacherId = teacherId,
                    StudentId = studentId
                });
            }
            db.Teachers.Add(teacher);
            db.StudentTeachers.AddRange(studentTeachers);

            db.SaveChanges();
            return Ok();
        }
    }
}
