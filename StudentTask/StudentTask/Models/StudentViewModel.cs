using System.Collections.Generic;

namespace StudentTask.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}