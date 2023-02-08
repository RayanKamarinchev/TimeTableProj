using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Classroom
    {
        [Required]
        [Key]
        public int Key { get; set; }
        [Required]
        public int Number { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
