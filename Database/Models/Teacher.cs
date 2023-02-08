using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }
        [Required]
        public virtual Subject Subject { get; set; }
        public virtual ICollection<TeacherBusy> TeacherBusies { get; set; }
        [Required]
        public virtual ICollection<ClassTeacher> ClassTeachers { get; set; }
        public virtual ICollection<SubjectCellTeacher> SubjectCells { get; set; }
    }
}
