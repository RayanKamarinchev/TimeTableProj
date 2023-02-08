using System.Collections.Generic;

namespace Model.DTOs
{
    public class TeacherDTO
    {
        public List<ClassDTO> classes { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public List<int> BusyDays { get; set; }
        public int id { get; set; }
        public TeacherDTO(List<ClassDTO> classes, string subject, string name, List<int> busyDays, int id)
        {
            this.BusyDays = busyDays;
            this.classes = classes;
            this.Subject = subject;
            this.Name = name;
            this.id = id;
        }

        public TeacherDTO()
        {
            
        }
    }
}
