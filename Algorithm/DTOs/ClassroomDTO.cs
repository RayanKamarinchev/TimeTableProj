using Database.Models;

namespace Model.DTOs
{
    public class ClassroomDTO
    {
        public int Number { get; set; }
        public SubjectType SubjectType { get; set; }
        public ClassDTO Class { get; set; }
    }
}
