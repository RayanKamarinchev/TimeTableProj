using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using MyTimeTable.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.DTOs;
using Database.Migrations;

namespace Controller
{
    public sealed class Controller
    {
        private static Controller instance;

        public List<ClassDTO> Classes { get; set; }
        public List<ClassroomDTO> Classrooms { get; set; }
        public List<TeacherDTO> Teachers { get; set; }
        public List<SubjectDTO> Subjects { get; set; }

        private Controller()
        {
            Classes = GetClasses();
            Classrooms = GetClassrooms();
            Teachers = GetTeachers();
            Subjects = GetSubjectsInfo();
        }

        public static Controller Instance()
        {
            if (instance is null)
            {
                instance = new Controller();
            }

            return instance;
        }

        private Context context = new Context();
        private Generator generator = new Generator();
        //public string images = "../../../Resources/";
        public string images = $"{AppContext.BaseDirectory}Images/";

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
                id = t.Id,
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

        private SubjectDTO ToSubjectDto(SubjectCell subjectCell)
        {
            return new SubjectDTO(subjectCell.Subject, ToTeacherDto(subjectCell.Teachers.Select(st=>st.Teacher).ToList()).ToArray(),
                                  subjectCell.TimesIn, subjectCell.ColorCode, subjectCell.SubjectType);
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
            return Classrooms.Count();
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
            return ToTeacherDto(context.Teachers
                                       .Include(t=>t.TeacherBusies)
                                       .Include(t=>t.ClassTeachers)
                                       .ThenInclude(ct=>ct.Class)
                                       .Include(t=>t.Subject).ToList());
        }

        public void RemoveTeacherByName(string name)
        {
            context.Teachers.Remove(context.Teachers.First(t => t.Name == name));
            context.SaveChanges();
        }
        public void UpdateTeacher(Teacher teacher)
        {
            context.Update(teacher);
            context.SaveChanges();
        }
        public void AddTeacher(Teacher teacher)
        {
            context.Add(teacher);
            context.SaveChanges();
        }

        public Teacher GetTeacherByName(string name)
        {
            return context.Teachers.First(t => t.Name == name);
        }


        //Subjects

        public List<SubjectDTO> GetSubjectsInfo()
        {
            return context.Subjects.Select(h => new SubjectDTO(h.Name, ToTeacherDto(h.Teachers.ToList()).ToArray(), 1, h.ColorCode, h.SubjectType)).ToList();
        }
        public string[] GetSubjectNames()
        {
            return Subjects.Select(h => h.Subject).ToArray();
        }

        public List<Subject> GetSubjectsWithIds()
        {
            return context.Subjects.ToList();
        }


        public Subject GetSubjectByName(string name)
        {
            return context.Subjects.First(h => h.Name == name);
        }

        public List<string[]> GetSubjectsColors()
        {
            return Subjects.Select(h => new string[] { h.Subject, h.ColorCode.ToString() }).ToList();
        }

        public void ChangeSubjectColor(string name, int color)
        {
            context.Subjects.FirstOrDefault(h => h.Name == name).ColorCode = color;
            context.SaveChanges();
        }

        public void UpdateSubjects(List<Subject> subjects, Subject[] deleted)
        {
            context.Subjects.UpdateRange(subjects);
            context.Subjects.RemoveRange(deleted);
            context.SaveChanges();
        }

        //Save Table
        public void SaveTable(List<TimeTable> data, List<ClassroomDTO>[,] classrooms)
        {
            List<SubjectCell> subjects = new List<SubjectCell>();
            if (data is null)
            {
                return;
            }
            foreach (var timeTable in data)
            {
                for (int i = 0; i < timeTable.days.Length; i++)
                {
                    int j = 0;
                    foreach (var subject in timeTable.days[i].Hours)
                    {
                        var teachers = context.Teachers
                                              .Where(t => subject.Teacher.Select(st => st.id).Contains(t.Id))
                                              .ToList();
                        subjects.Add(new SubjectCell()
                        {
                            Class = context.Classes.FirstOrDefault(c=>c.Name == timeTable.Class.Name),
                            ColorCode = subject.ColorCode,
                            Day = i,
                            PositionInDay = j,
                            Subject = subject.Subject,
                            SubjectType = subject.SubjectType,
                            Teachers = teachers.Select(t=> new SubjectCellTeacher()
                            {
                                Teacher = t
                            }).ToList(),
                            TimesIn = subject.TimesIn
                        });
                        j++;
                    }
                }
            }
            context.Timetable.RemoveRange(context.Timetable);
            context.Timetable.AddRange(subjects);

            List<ClassroomCell> classroomCells = new List<ClassroomCell>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    foreach (var classroom in classrooms[i,j])
                    {
                        classroomCells.Add(new ClassroomCell()
                        {
                            Classroom = context.Classrooms.FirstOrDefault(c => c.Number == classroom.Number),
                            Day = i,
                            PositionInDay = j,
                            Class = context.Classes.FirstOrDefault(c=>c.Name == classroom.Class.Name)
                        });
                    }
                }
            }
            context.ClassroomArrangement.RemoveRange(context.ClassroomArrangement);
            context.ClassroomArrangement.AddRange(classroomCells);
            context.SaveChanges();
        }

        public Tuple<List<TimeTable>, List<ClassroomDTO>[,]> GetSavedTable()
        {
            List<TimeTable> timetable = new List<TimeTable>();
            var timetableDb = context.Timetable.Include(t=>t.Class)
                                     .Include(t=>t.Teachers).ToList();
            if (!timetableDb.Any())
            {
                throw new Exception();
            }
            foreach (var classCell in timetableDb.GroupBy(t=>t.Class)
                                                 .OrderBy(t=>int.Parse(t.Key.Name.Substring(0, t.Key.Name.Length-1)))
                                                 .ThenBy(t => t.Key.Name[^1]))
            {
                Day[] days = new Day[5];
                for (int i = 0; i < 5; i++)
                {
                    days[i] = new Day();
                }
                foreach (var subjectCell in classCell.OrderBy(c=>c.PositionInDay))
                {
                    days[subjectCell.Day].Hours.Add(ToSubjectDto(subjectCell));
                }

                List <Class> clas = new List<Class>();
                clas.Add(classCell.Key);
                timetable.Add(new TimeTable(days, ToClassDto(clas)[0]));
            }

            List<ClassroomDTO>[,] classroomArangement = new List<ClassroomDTO>[5, 7];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    classroomArangement[i, j] = new List<ClassroomDTO>();
                }
            }
            var classroomsDb = context.ClassroomArrangement.ToList();
            foreach (var classroomCell in classroomsDb)
            {
                List<Classroom> classrooms = new List<Classroom>();
                classrooms.Add(classroomCell.Classroom);
                var classroom = ToClassroomDto(classrooms)[0];
                List<Class> classes = new List<Class>();
                classes.Add(classroomCell.Class);
                classroom.Class = ToClassDto(classes)[0];
                classroomArangement[classroomCell.Day, classroomCell.PositionInDay].Add(classroom);
            }
            return new Tuple<List<TimeTable>, List<ClassroomDTO>[,]>(timetable, classroomArangement);
        }
    }
}
