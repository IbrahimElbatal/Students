using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTask.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {

    }
    public class StudentMetaData
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [UIHint("Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Governorate")]
        public int GovernorateId { get; set; }

        [Display(Name = "Neighborhood")]
        public Nullable<int> NeighborhoodId { get; set; }

        [Required]
        [Display(Name = "Field")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }

    }
}