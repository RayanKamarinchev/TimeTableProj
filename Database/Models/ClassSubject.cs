using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class ClassSubject
    {
        [Required]
        public int TimesPerWeek { get; set; }
        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
