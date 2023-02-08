using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class SubjectCellTeacher
    {
        [ForeignKey(nameof(SubjectCellId))]
        public SubjectCell SubjectCell { get; set; }
        [Required]
        public int SubjectCellId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
        [Required]
        public int TeacherId { get; set; }
    }
}
