using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTask.Models
{
    [MetadataType(typeof(TeacherMetaData))]
    public partial class Teacher
    {

    }
    public class TeacherMetaData
    {

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [UIHint("Date")]
        public DateTime BirthDate { get; set; }

    }
}