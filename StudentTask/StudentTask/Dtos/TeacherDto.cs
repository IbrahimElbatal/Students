using System;

namespace StudentTask.Dtos
{
    public class TeacherDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int[] StudentIds { get; set; }
    }
}