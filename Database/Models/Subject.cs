using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Subject
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ColorCode { get; set; } = System.Drawing.Color.White.ToArgb();
        public virtual ICollection<Teacher> Teachers { get; set; }
        [Required]
        public ICollection<ClassSubject> ClassSubjects { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
