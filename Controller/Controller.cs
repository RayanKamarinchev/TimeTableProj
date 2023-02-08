using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;
using Database.DTOs;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using MyTimeTable.Algorithm;
using MyTimeTable.Models;

namespace Controller
{
    public class Controller
    {
        private Context context = new Context();
        private Generator generator = new Generator();

        //Generate
        public Tuple<List<TimeTable>, List<ClassroomDTO>[,]> GenerateTable(List<ClassDTO> classes, List<TeacherDTO> teachers, List<ClassroomDTO> classrooms, List<SubjectDTO> subjects)
        {
            return generator.Generate(classes, teachers, classrooms, subjects);
        }

        //Mappings
        static List<TeacherDTO> ToTeacherDto(List<Teacher> teachers)
        {
            return teachers.Select(t => new TeacherDTO()
            {
                Name = t.Name,
                Subject = t.Subject.Name,
                BusyDays = t.TeacherBusies is null ? new List<int>() : t.TeacherBusies.Select(tb => tb.Day)
                                                                        .ToList(),
                classes = t.ClassTeachers.Select(ct => new ClassDTO()
                {
                    Name = ct.Class.Name
                }).ToList()
            }).ToList();
        }

        static List<ClassDTO> ToClassDto(List<Class> dbClasses)
        {
            List<ClassDTO> classes = new List<ClassDTO>();
            foreach (var c in dbClasses)
            {
                var d = new ClassDTO();
                d.CountOfSubjectsForWeek = new Dictionary<string, int>();
                foreach (var cs in c.SubjectTimes)
                {
                    d.CountOfSubjectsForWeek.Add(cs.Subject.Name, cs.TimesPerWeek);
                }
                classes.Add(new ClassDTO(c.Name, d.CountOfSubjectsForWeek, c.ClassTeachers.Select(ct => ct.Teacher).Select(t => new TeacherDTO()
                {
                    Name = t.Name,
                    Subject = t.Subject.Name
                }).ToList()));
            }

            return classes;
        }

        static List<ClassroomDTO> ToClassroomDto(List<Classroom> classrooms)
        {
            return classrooms.Select(c => new ClassroomDTO()
            {
                Number = c.Number,
                SubjectType = c.SubjectType
            })
                             .OrderBy(c => c.SubjectType)
                             .ToList();
        }

        //Classes

        public List<ClassDTO> GetClasses()
        {
            List<Class> dbClasses = context.Classes
                                    .Include(c => c.SubjectTimes)
                                    .ThenInclude(c => c.Subject)
                                    .Include(c => c.ClassTeachers)
                                    .ThenInclude(ct => ct.Teacher)
                                    .ThenInclude(t => t.Subject)
                                    .ToList();
            var classes = ToClassDto(dbClasses);
            //order by class number and then by the letter
            return classes.OrderBy(c => int.Parse(c.Name.Substring(0, c.Name.Length - 1))).ThenBy(c => c.Name[^1]).ToList();
        }

        public Class GetClassByName(string name)
        {
            return context.Classes.First(h => h.Name == name);
        }


        public void AddClass(Class clas)
        {
            context.Classes.Add(clas);
            context.SaveChanges();
        }

        public void AddClassSubject(Class clas, Subject subj)
        {
            context.ClassSubjects.Add(new ClassSubject()
            {
                Class = clas,
                Subject = subj
            });
            context.SaveChanges();
        }

        //Classrooms
        public List<ClassroomDTO> GetClassrooms()
        {
            return ToClassroomDto(context.Classrooms.ToList());
        }

        public void FixClassroomsNumericalOrder()
        {
            List<Classroom> cabinets = context.Classrooms.OrderBy(c => c.SubjectType).ToList();
            for (int i = 0; i < cabinets.Count; i++)
            {
                cabinets[i].Number = i + 1;
            }

            context.SaveChanges();
        }

        public int GetClassroomsCount()
        {
            return context.Classrooms.Count();
        }


        public void RemoveClassroomByNumber(int number)
        {
            context.Classrooms.Remove(context.Classrooms.First(c => c.Number == number));
            context.SaveChanges();
        }

        public void AddClassroomWithSubject(int num, SubjectType subject)
        {
            context.Classrooms.Add(new Classroom()
            {
                Number = num,
                SubjectType = subject
            });
            context.SaveChanges();
        }
        public void AddClassroomWithoutSubject(int num)
        {
            context.Classrooms.Add(new Classroom()
            {
                Number = num
            });
            context.SaveChanges();
        }

        //Teachers

        public List<TeacherDTO> GetTeachers()
        {
            return ToTeacherDto(context.Teachers.ToList());
        }

        public void RemoveTeacherByName(string name)
        {
            context.Teachers.Remove(context.Teachers.First(t => t.Name == name));
            context.SaveChanges();
        }
        public void AddTeacher(Teacher teacher)
        {
            context.Add(teacher);
            context.SaveChanges();
        }

        //Subjects

        public string[] GetSubjects()
        {
            return context.Hours.Select(h => h.Name).ToArray();
        }

        public List<Subject> GetSubjectsWithIds()
        {
            return context.Hours.ToList();
        }

        public List<SubjectDTO> GetSubjectsInfo()
        {
            return context.Hours.Select(h => new SubjectDTO(h.Name, ToTeacherDto(h.Teachers.ToList()).ToArray(), 1, h.ColorCode, h.SubjectType)).ToList();
        }

        public Subject GetSubjectByName(string name)
        {
            return context.Hours.First(h => h.Name == name);
        }

        public List<string[]> GetSubjectsColors()
        {
            return context.Hours.Select(h => new string[] { h.Name, h.ColorCode.ToString() }).ToList();
        }

        public void ChangeSubjectColor(string name, int color)
        {
            context.Hours.FirstOrDefault(h => h.Name == name).ColorCode = color;
            context.SaveChanges();
        }

        public void UpdateSubjects(List<Subject> subjects, Subject[] deleted)
        {
            context.Hours.UpdateRange(subjects);
            context.Hours.RemoveRange(deleted);
            context.SaveChanges();
        }
    }
}
