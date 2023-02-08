using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Model.DTOs
{
    public class ClassDTO
    {
        public string Name { get; set; }
        public int Subjects;
        [JsonIgnore]
        //Subject groups devided by the days in the week
        public Dictionary<string, List<int>> SubjectGroups { get; set; }
        public Dictionary<string, int> CountOfSubjectsForWeek { get; set; }
        public List<TeacherDTO> Teachers { get; set; }

        public ClassDTO()
        {
            
        }
        public ClassDTO(string name, Dictionary<string, int> hours, List<TeacherDTO> teachers)
        {
            this.Teachers = teachers;
            this.Name = name;
            ReOrganize(hours);
        }
        public ClassDTO(string name, Dictionary<string, int> hours)
        {
            this.Name = name;
            ReOrganize(hours);
        }

        private void ReOrganize(Dictionary<string, int> hours)
        {
            Random rand = new Random();
            hours = hours.OrderBy(d => rand.Next()).ToDictionary(x => x.Key, x => x.Value);
            this.SubjectGroups = new Dictionary<string, List<int>>();
            foreach (var dup in hours)
            {
                int[] hoursPerDay;
                switch (dup.Value)
                {
                    case 1:
                        hoursPerDay = new[] { 1 };
                        break;
                    case 2:
                        hoursPerDay = new[] { 1, 1 };
                        break;
                    case 3:
                        hoursPerDay = new[] { 1, 1, 1 };
                        break;
                    case 4:
                        hoursPerDay = new[] { 2, 1, 1 };
                        break;
                    case 5:
                        hoursPerDay = new[] { 2, 1, 1, 1 };
                        break;
                    case 6:
                        hoursPerDay = new[] { 2, 2, 1, 1 };
                        break;
                    case 7:
                        hoursPerDay = new[] { 2, 2, 1, 1, 1 };
                        break;
                    default:
                        int avg = dup.Value / 5;
                        hoursPerDay = new[]
                        {
                            avg + (dup.Value % 5 >= 1 ? 1 : 0), avg + (dup.Value % 5 >= 2 ? 1 : 0),
                            avg + (dup.Value % 5 >= 3 ? 1 : 0), avg + (dup.Value % 5 >= 4 ? 1 : 0), avg
                        };
                        break;
                }
                this.SubjectGroups.Add(dup.Key, hoursPerDay.ToList());
            }

            CountOfSubjectsForWeek = hours;
            this.Subjects = hours.Sum(h => h.Value);
        }
    }
}
