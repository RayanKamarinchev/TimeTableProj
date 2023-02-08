using Database.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Model.DTOs;

namespace MyTimeTable.Algorithm
{
    public class Generator
    {
        static List<ClassroomDTO>[,] classroomsArrangment = new List<ClassroomDTO>[5, 7];
        private static List<ClassroomDTO> allClassrooms = new List<ClassroomDTO>();
        private static List<ClassroomDTO> availableClassrooms = new List<ClassroomDTO>();
        static List<TimeTable> timetable = new List<TimeTable>();
        private static List<MistakeIndex> mistakes = new List<MistakeIndex>();
        static Random rand = new Random();
        private static Dictionary<string, int> classClassrooms = new Dictionary<string, int>();

        //Gives count of subjects for each day in the week on average
        static List<int> RandomizeNumberOfHoursPerDay(int hours)
        {
            List<int> options = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                options.Add((int)Math.Round((double)hours / (5 - i) + 0.2));
                hours -= options[i];
            }

            return options;
        }


        //Random population
        static TimeTable RandWeek(List<TeacherDTO> teachers, ClassDTO clas, List<SubjectDTO> allSubjects)
        {

            ClassDTO copy = new ClassDTO(clas.Name, clas.CountOfSubjectsForWeek);
            Day[] days = new Day[5];
            for (int j = 0; j < 5; j++)
            {
                days[j] = new Day();
            }

            int subjects = copy.Subjects;

            KeyValuePair<string, int> mostCommonSubject;
            List<int> options = new List<int>() { 0, 1, 2, 3, 4 };
            int i = 0;
            List<int> subjectsForToday = RandomizeNumberOfHoursPerDay(subjects);
            do
            {
                //resets the options order so the table can be populated randomly
                if (i % options.Count == 0)
                {
                    options = options.OrderBy(o => rand.Next()).ToList();
                    i = 0;
                }

                //Gets the most common subject
                mostCommonSubject = copy.CountOfSubjectsForWeek.Aggregate((l, r) => l.Value > r.Value ? l : r);

                var subjType = allSubjects.Find(s => s.Subject == mostCommonSubject.Key).SubjectType;
                if (subjType == SubjectType.Компютърен)
                {
                    List<int> subjCounts = copy.SubjectGroups[mostCommonSubject.Key];
                    int ones = 0;
                    foreach (var subjCount in subjCounts)
                    {
                        if (subjCount==1)
                        {
                            ones++;
                        }
                    }

                    for (int j = 2; j <= ones; j+=2)
                    {
                        subjCounts.Remove(1);
                        subjCounts.Remove(1);
                        subjCounts.Add(2);
                    }

                }
                //Creates subjectDto based on the most common subject 
                SubjectDTO hour = new SubjectDTO(mostCommonSubject.Key,
                                     teachers.Where(t => t.Subject == mostCommonSubject.Key &&
                                                         t.classes.Any(c => c.Name == copy.Name))
                                             .ToArray()
                                     , copy.SubjectGroups[mostCommonSubject.Key].Max(),
                                                Color.White.ToArgb(), subjType);
                //Removes the subject from the groups
                copy.SubjectGroups[mostCommonSubject.Key].Remove(hour.TimesIn);
                //If there are no subjects left in the group, remove the group
                if (copy.SubjectGroups[mostCommonSubject.Key].Count == 0)
                {
                    copy.CountOfSubjectsForWeek[mostCommonSubject.Key] = 0;
                }

                int tries = GetPossibleSubjectPositions(hour);
                if (tries == -1)
                {
                    return RandWeek(teachers, clas, allSubjects);
                }

                //Checks if we already have enough subjects for the given day
                days[options[tries]].Hours.Add(hour);
                int sum = days[options[tries]].Sum;
                bool success = subjectsForToday.Remove(sum);
                if (success)
                {
                    options.Remove(options[tries]);
                    if (options.Count == 0)
                    {
                        break;
                    }

                    if (i % options.Count == 0)
                    {
                        options = options.OrderBy(o => rand.Next()).ToList();
                        i = 0;
                    }
                }

                if (sum > subjectsForToday[0])
                {
                    return RandWeek(teachers, clas, allSubjects);
                }
                i++;
            } while (subjectsForToday.Count != 0);

            //Shuffles the subjects
            for (int j = 0; j < 5; j++)
            {
                bool again = true;
                while (again)
                {
                    days[j].Hours = days[j].Hours.OrderBy(h => rand.Next()).ToList();
                    again = false;
                    for (int k = 0; k < days[j].IndexedHours.Count; k++)
                    {
                        //checks if the teacher is busy in that day
                        if (days[j].IndexedHours[k].Teacher.Any(t => t.BusyDays.Contains((j + 1) * 10 + k + 1)))
                        {
                            again = true;
                        }
                    }
                }
            }

            if (subjects != days.Sum(d => d.Sum))
            {
                return RandWeek(teachers, clas, allSubjects);
            }

            return new TimeTable(days, clas);

            bool InRange(int sum)
            {
                return sum <= subjectsForToday.Max();
            }

            int GetPossibleSubjectPositions(SubjectDTO hour)
            {
                int counter = 0;
                int tries = i;
                //Loops if there is already the same subject in the day we are looking at or when it will create too many subjects for the day
                while (days[options[tries]].Hours.Any(h => h.Subject == hour.Subject) ||
                       !InRange(days[options[tries]].Sum + hour.TimesIn))
                {
                    tries++;
                    if (tries == options.Count)
                    {
                        if (tries == 1 || counter > 5)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                for (int k = 0; k < days[j].Hours.Count; k++)
                                {
                                    //checks if its okay to add the subject
                                    if (!days[options[0]].Hours.Any(s => s.Subject == days[j].Hours[k].Subject) &&
                                        InRange(days[options[0]].Sum + days[j].Hours[k].TimesIn))
                                    {
                                        SubjectDTO removedHour = days[j].Hours[k];
                                        days[j].Hours.RemoveAt(k);
                                        days[options[0]].Hours.Add(removedHour);
                                        return 0;
                                    }
                                }
                            }
                        }
                        //Impossible to add the subject
                        return -1;
                    }

                    counter++;
                }

                return tries;
            }
        }

        //Get positions of the table mistakes, such as subject/teachers overlap, the teacher is busy this day or not enough classrooms
        static void FindMistakes()
        {
            mistakes = new List<MistakeIndex>();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    availableClassrooms = new List<ClassroomDTO>(allClassrooms);
                    Random rand = new Random();
                    //Gets all subjects in a particular time for each class
                    var allSubjectsAtTime = timetable
                                            .Where(t => t.days[i].Sum>j)
                                            .Select(t => new
                                            {
                                                subj = t.days[i].IndexedHours[j],
                                                clas = t.Class,
                                                idx = t.Class
                                            })
                                            .OrderBy(x=>rand.Next())
                                            .ToArray();
                    //Classroom mistakes
                    bool skip = false;
                    int ind = 0;
                    foreach (var subj in allSubjectsAtTime)
                    {
                        int k = timetable.IndexOf(timetable.Find(t => t.Class == subj.clas));
                        var subjType = allSubjectsAtTime[ind].subj.SubjectType;
                        ClassroomDTO possibility;
                        if (subjType == SubjectType.Обикновен)
                        {
                            if (classClassrooms.ContainsKey(allSubjectsAtTime[ind].clas.Name))
                            {
                                possibility = new ClassroomDTO()
                                {
                                    Class = allSubjectsAtTime[ind].clas,
                                    Number = classClassrooms[allSubjectsAtTime[ind].clas.Name],
                                    SubjectType = subjType
                                };
                            }
                            else
                            {
                                int num = availableClassrooms.FirstOrDefault(c => c.SubjectType == subjType
                                                                               && !classClassrooms.ContainsValue(c.Number)).Number;
                                classClassrooms.Add(allSubjectsAtTime[ind].clas.Name, num);
                                possibility = new ClassroomDTO()
                                {
                                    Class = allSubjectsAtTime[ind].clas,
                                    Number = num,
                                    SubjectType = subjType
                                };
                            }
                        }
                        else
                        {
                            //If its outside then there is no need for classroom
                            if (subjType == SubjectType.НаОткрито)
                            {
                                ind++;
                                continue;
                            }
                            //Searches for available classrooms
                            ClassroomDTO copy = availableClassrooms.FirstOrDefault(c => c.SubjectType == subjType);
                            //if none, combine biology, chemistry and physics into science
                            if (copy is null && (subjType == SubjectType.Биология || subjType == SubjectType.Физика || subjType == SubjectType.Химия))
                            {
                                copy = availableClassrooms.FirstOrDefault(c => c.SubjectType == SubjectType.ПриродниНауки);
                            }
                            //if there is no classroom available
                            if (copy == null)
                            {
                                int originalClassIndex = k;
                                int repeatedHourGroupPosition = timetable[k]
                                                                .days[i]
                                                                .Hours
                                                                .FindIndex(h => h.Subject == timetable[k]
                                                                                             .days[i]
                                                                                             .IndexedHours[j].Subject);
                                int originalHourGroupPosition = repeatedHourGroupPosition;
                                List<int> busyDays = new List<int>();
                                mistakes.Add(new MistakeIndex(i, j, k, originalClassIndex,
                                                      repeatedHourGroupPosition, originalHourGroupPosition, busyDays));
                                skip = true;
                                break;
                            }
                            //adds the classroom
                            possibility = new ClassroomDTO()
                            {
                                Class = allSubjectsAtTime[ind].clas,
                                Number = copy.Number,
                                SubjectType = copy.SubjectType
                            };
                        }

                        classroomsArrangment[i, j].Add(possibility);
                        availableClassrooms.Remove(availableClassrooms.Find(c=>c.Number==possibility.Number));
                        ind++;
                    }

                    if (skip)
                    {
                        continue;
                    }

                    //get teachers in particular hour
                    var first = timetable
                                .Where(t => t.days[i].Sum > j)
                                .Select(t => new
                                {
                                    teacher = t.days[i].IndexedHours[j].Teacher,
                                    indexOfClass = timetable.IndexOf(t)
                                }).ToList();
                    List<string> repeats = new List<string>();
                    for (int k = 0; k < first.Count; k++)
                    {
                        var teacher = repeats.FirstOrDefault(r => first[k].teacher.Any(t => t.Name == r.Split("/")[0]));
                        bool teacherIsBusy = first[k].teacher.Any(t => t.BusyDays.Any(b => b == (i + 1) * 10 + j + 1));
                        //If teacher is busy
                        if (teacherIsBusy)
                        {
                            int originalClassIndex = first[k].indexOfClass;
                            int repeatedHourGroupPosition = timetable[first[k].indexOfClass]
                                                            .days[i]
                                                            .Hours
                                                            .FindIndex(h => h.Subject == first[k].teacher[0].Subject);
                            int originalHourGroupPosition = repeatedHourGroupPosition;
                            List<int> busyDays = new List<int>();
                            foreach (var t in first[k].teacher)
                            {
                                foreach (var b in t.BusyDays)
                                {
                                    if (b / 10 == i + 1)
                                    {
                                        busyDays.Add(b%10);
                                    }
                                }
                            }
                            mistakes.Add(new MistakeIndex(i, j, first[k].indexOfClass, originalClassIndex,
                                                  repeatedHourGroupPosition, originalHourGroupPosition, busyDays));
                        }
                        //if there is a teacher repetition
                        else if ((mistakes.Count == 0 && teacher != null))
                        {
                            int originalClassIndex = int.Parse(teacher.Split("/")[1]);
                            int repeatedHourGroupPosition = timetable[first[k].indexOfClass]
                                                            .days[i]
                                                            .Hours
                                                            .FindIndex(h => h.Subject == first[k].teacher[0].Subject);
                            int originalHourGroupPosition = timetable[int.Parse(teacher.Split("/")[1])]
                                                            .days[i]
                                                            .Hours
                                                            .FindIndex( h => h.Teacher.Any(t => first[k].teacher.Any( k =>k.Name ==t.Name)));
                            mistakes.Add(new MistakeIndex(i, j, first[k].indexOfClass, originalClassIndex, 
                                                  repeatedHourGroupPosition, originalHourGroupPosition));
                        }
                        else if (teacher != null && !(mistakes[^1].RepeatedClassIndex != first[k].indexOfClass &&
                                                    mistakes[^1].DayOfTheWeek != i &&
                                                    mistakes[^1].HourPositionInDay != j))
                        {
                            mistakes.Add(new MistakeIndex(i, j, first[k].indexOfClass, int.Parse(teacher.Split("/")[1]),
                                                  timetable[first[k].indexOfClass].days[i].Hours
                                                                                  .FindIndex(h => h.Subject == first[k].teacher[0].Subject),
                                                  timetable[int.Parse(teacher.Split("/")[1])].days[i].Hours
                                                                                           .FindIndex(h => h.Teacher.Any(t => first[k].teacher.Any(k => k.Name ==t.Name)))));
                        }
                        else
                        {
                            //If there wasnt found teacher add it to the list of teachers
                            repeats.AddRange(first[k].teacher.Select(r => r.Name)
                                                     .Select(t => t += "/" + first[k].indexOfClass));
                        }
                    }
                }
            }
        }

        static bool CheckIsValid(int i, int j, TeacherDTO[] teachers)
        {
            var teachersAtThisTime = timetable
                        .Where(t => t.days[i].Sum > j)
                        .Select(t => new
                        {
                            teacher = t.days[i].IndexedHours[j].Teacher,
                            indexOfClass = timetable.IndexOf(t)
                        }).ToList();
            bool isTeacherBusy = !teachers.Any(t => t.BusyDays.Any(i => i == (i + 1) * 10 + j + 1));
            bool areThereTeacherOverlaps =
                teachersAtThisTime.Count(f => f.teacher.Any(t => teachers.Any(r => r.Name == t.Name))) == 0;
            return areThereTeacherOverlaps && isTeacherBusy;
        }

        public Tuple<List<TimeTable>, List<ClassroomDTO>[,]> Generate(List<ClassDTO> oldClasses, List<TeacherDTO> teachers, List<ClassroomDTO> classrooms, List<SubjectDTO> subjects)
        {
            void SwitchSubjects(string state)
            {
                //finds mistakes
                FindMistakes();
                ResetClassrooms();
                //loops through all the mistakes found
                foreach (var index in mistakes)
                {
                    var repeatedHoursList = timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek].Hours;
                    var originalHoursList = timetable[index.OriginalClassIndex].days[index.DayOfTheWeek].Hours;
                    SubjectDTO theHour = repeatedHoursList[index.RepeatedHourGroupPosition];
                    SubjectDTO first = originalHoursList[index.OriginalHourGroupPosition];
                    List<int> hourValids = new List<int>();
                    List<int> firstHourValids = new List<int>();
                    int pos = 0;
                    //Tried to change the positon of the subject for one of the days
                    for (int i = 0; i < repeatedHoursList.Count; i++)
                    {
                        //if the current position is the same as the subject`s position, skip it
                        if (i == index.RepeatedHourGroupPosition)
                        {
                            pos += repeatedHoursList[i].TimesIn;
                            continue;
                        }
                        if (CheckIsValid(index.DayOfTheWeek, pos,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[index.HourPositionInDay]
                                                                           .Teacher)
                         && CheckIsValid(index.DayOfTheWeek, index.HourPositionInDay,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[pos].Teacher) &&
                            !index.Busies.Contains(i + 1))
                        {
                            hourValids.Add(i);
                        }

                        pos += repeatedHoursList[i].TimesIn;
                    }

                    //Tried to change the positon of the subject for the other day
                    pos = 0;
                    for (int i = 0; i < originalHoursList.Count; i++)
                    {
                        //if the current position is the same as the subject`s position, skip it
                        if (i == index.OriginalHourGroupPosition)
                        {
                            pos += originalHoursList[i].TimesIn;
                            continue;
                        }

                        index.OriginalHourGroupPosition = i;
                        if (CheckIsValid(index.DayOfTheWeek, pos,
                                        timetable[index.OriginalClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[index.HourPositionInDay]
                                                                           .Teacher)
                         && CheckIsValid(index.DayOfTheWeek, index.HourPositionInDay,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[pos].Teacher) &&
                            !index.Busies.Contains(i + 1))
                        {
                            firstHourValids.Add(i);
                        }
                    }
                    //Moves the subject to another position
                    if (hourValids.Count != 0)
                    {
                        repeatedHoursList.Remove(theHour);
                        repeatedHoursList.Insert(hourValids[0], theHour);
                    }
                    else if (firstHourValids.Count != 0)
                    {
                        originalHoursList.Remove(first);
                        originalHoursList.Insert(firstHourValids[0], first);
                    }
                    else
                    {
                        //If there is no possible solution by moving the overlapping subjects then re-populate the table
                        if (state=="searching")
                        {
                            timetable[index.RepeatedClassIndex] = RandWeek(
                                teachers, oldClasses[index.RepeatedClassIndex], subjects);
                            break;
                        }
                    }
                }
            }
            ResetClassrooms();
            allClassrooms = classrooms;
            //how many times we will loop through the mistakes to find possible solution
            int timesUntilReset = 80;
            //the number of mistakes we are trying to reach, zero for no mistakes
            int target = 0;
            do
            {
                timetable = new List<TimeTable>();
                mistakes = new List<MistakeIndex>();
                List<ClassDTO> classses = new List<ClassDTO>(oldClasses);

                //Generate Random population
                foreach (var clas in classses)
                {
                    timetable.Add(RandWeek(teachers, clas, subjects));
                }

                int last = 0;
                int repeats = 0;
                FindMistakes();
                ResetClassrooms();
                int t = 0;
                //Replace
                while (true)
                {
                    SwitchSubjects("searching");
                    t++;
                    if (mistakes.Count <= target)
                    {
                        break;
                    }
                    //if a number of mistakes is being repeated then stop generating (mostly happens when there is only 1 or 2 mistakes left)
                    if (last == mistakes.Count)
                    {
                        repeats++;
                    }
                    else
                    {
                        repeats = 0;
                    }

                    if (repeats > 3)
                    {
                        break;
                    }
                    last = mistakes.Count;
                    if (t == timesUntilReset)
                    {
                        break;
                    }
                }
                //Test
                FindMistakes();
                //we reset the classrooms for every generation but if we accomplished our goal then we will save the classrooms
                if (mistakes.Count > target)
                {
                    ResetClassrooms();
                }
            } while (mistakes.Count > target);
            return new Tuple<List<TimeTable>, List<ClassroomDTO>[,]>(timetable, classroomsArrangment);
        }

        private void ResetClassrooms()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    classroomsArrangment[i, j] = new List<ClassroomDTO>();
                }
            }
        }
    }
}
