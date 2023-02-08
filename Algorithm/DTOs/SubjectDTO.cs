using Database.Models;

namespace Model.DTOs
{
    public class SubjectDTO
    {
        public string Subject { get; set; }
        public int TimesIn { get; set; }
        public TeacherDTO[] Teacher { get; set; }
        public int ColorCode { get; set; }
        public SubjectType SubjectType { get; set; }
        public SubjectDTO(string subject, TeacherDTO[] teacher, int timesIn, int colorCode)
        {
            Subject = subject;
            Teacher = teacher;
            TimesIn = timesIn;
            ColorCode = colorCode;
        }
        public SubjectDTO(string subject, TeacherDTO[] teacher, int timesIn, int colorCode, SubjectType subjectType)
        {
            Subject = subject;
            Teacher = teacher;
            TimesIn = timesIn;
            ColorCode = colorCode;
            SubjectType = subjectType;
        }

        public SubjectDTO(string subject, TeacherDTO[] teacher, int timesIn)
        {
            Subject = subject;
            Teacher = teacher;
            TimesIn = timesIn;
        }
    }
}
