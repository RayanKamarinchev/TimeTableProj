using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class TeacherBusy
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        [Required]
        public int Day { get; set; }
        public Teacher Teacher { get; set; }
    }
}
