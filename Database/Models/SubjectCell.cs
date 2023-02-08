using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class SubjectCell
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ICollection<SubjectCellTeacher> Teachers { get; set; }
        [Required]
        public SubjectType SubjectType { get; set; }
        [Required]
        public int ColorCode { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int TimesIn { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int PositionInDay { get; set; }
        [Required]
        public Class Class { get; set; }
    }
}
