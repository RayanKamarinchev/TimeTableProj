using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Class
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ICollection<ClassSubject> SubjectTimes { get; set; }
        [Required]
        public virtual ICollection<ClassTeacher> ClassTeachers { get; set; }
    }
}
