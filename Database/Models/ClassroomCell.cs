using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class ClassroomCell
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Classroom Classroom { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int PositionInDay { get; set; }

        public Class Class { get; set; }
        public int? ClassId { get; set; }
    }
}
