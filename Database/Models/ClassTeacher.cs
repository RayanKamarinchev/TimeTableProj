using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class ClassTeacher
    {
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
