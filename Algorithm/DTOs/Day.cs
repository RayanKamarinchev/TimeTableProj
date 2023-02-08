using System.Collections.Generic;

namespace Model.DTOs
{
    public class Day
    {
        public List<SubjectDTO> Hours { get; set; }

        public int Sum
        {
            get
            {
                return IndexedHours.Count;
            }
        }

        public List<SubjectDTO> IndexedHours
        {
            get
            {
                List<SubjectDTO> res = new List<SubjectDTO>();
                foreach (var h in Hours)
                {
                    SubjectDTO copy = new SubjectDTO(h.Subject, h.Teacher, 1, h.ColorCode, h.SubjectType);
                    List<SubjectDTO> times = new List<SubjectDTO>();
                    for (int i = 0; i < h.TimesIn; i++)
                    {
                        times.Add(copy);
                    }

                    res.AddRange(times);
                }

                return res;
            }
        }

        public Day()
        {
            Hours = new List<SubjectDTO>();
        }
        public Day(List<SubjectDTO> hours)
        {
            Hours = hours;
        }
    }
}
