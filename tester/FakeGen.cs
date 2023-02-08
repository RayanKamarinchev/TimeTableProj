using MyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Database;
using Database.DTOs;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MyTimeTable
{
    public class Generator
    {
        static List<ClassroomDTO>[,] CabinetsArrangment = new List<ClassroomDTO>[5, 7];
        private static List<ClassroomDTO> allCabinets = new List<ClassroomDTO>();
        private static List<ClassroomDTO> availableCabinets = new List<ClassroomDTO>();
        static List<TimeTable> timetable = new List<TimeTable>();
        private static List<Index> indexes = new List<Index>();

        static Random rand = new Random();

        static List<int> HoursToday(int hours)
        {
            List<int> options = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                options.Add((int)Math.Round((double)hours / (5 - i) + 0.2));
                hours -= options[i];
            }

            return options;
        }



        static TimeTable RandWeek(List<TeacherDTO> teachers, ClassDTO clas)
        {
            ClassDTO copy = new ClassDTO(clas.Name, clas.HourTimes);
            Day[] days = new Day[5];
            for (int j = 0; j < 5; j++)
            {
                days[j] = new Day();
            }

            int hours = copy.Hours;

            KeyValuePair<string, int> maxHour;
            List<int> options = new List<int>() { 0, 1, 2, 3, 4 };
            int i = 0;
            List<int> hoursForToday = HoursToday(hours);
            do
            {
                if (i % options.Count == 0)
                {
                    options = options.OrderBy(o => rand.Next()).ToList();
                    i = 0;
                }

                maxHour = copy.HourTimes.Aggregate((l, r) => l.Value > r.Value ? l : r);
                SubjectDTO hour = new SubjectDTO(maxHour.Key,
                                     teachers.Where(t => t.Subject == maxHour.Key &&
                                                         t.classes.Any(c => c.Name == copy.Name))
                                             .ToArray()
                                     , copy.PickHours[maxHour.Key].Max());
                copy.PickHours[maxHour.Key].Remove(hour.TimesIn);
                if (copy.PickHours[maxHour.Key].Count == 0)
                {
                    copy.HourTimes[maxHour.Key] = 0;
                }

                int tries = SeePossibilities(hour);
                if (tries == -1)
                {
                    return RandWeek(teachers, clas);
                }

                days[options[tries]].Hours.Add(hour);
                int sum = days[options[tries]].Sum;
                bool succes = hoursForToday.Remove(sum);
                if (succes)
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

                if (sum > hoursForToday[0])
                {
                    return RandWeek(teachers, clas);
                }
                i++;
            } while (hoursForToday.Count != 0);

            for (int j = 0; j < 5; j++)
            {
                bool again = true;
                while (again)
                {
                    days[j].Hours = days[j].Hours.OrderBy(h => rand.Next()).ToList();
                    again = false;
                    for (int k = 0; k < days[j].IndexedHours.Count; k++)
                    {
                        if (days[j].IndexedHours[k].Teacher.Any(t => t.BusyDays.Contains((j + 1) * 10 + k + 1)))
                        {
                            again = true;
                        }
                    }
                }
            }

            if (hours != days.Sum(d => d.Sum))
            {
                return RandWeek(teachers, clas);
            }

            return new TimeTable(days, clas);

            bool InRange(int sum)
            {
                return sum <= hoursForToday.Max();
            }

            int SeePossibilities(SubjectDTO hour)
            {
                //BUG: Infinite loop
                int counter = 0;
                int tries = i;
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

                        return -1;
                    }

                    counter++;
                }

                return tries;
            }
        }

        static void FindMistakesALL()
        {
            indexes = new List<Index>();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    availableCabinets = new List<ClassroomDTO>(allCabinets);
                    var allSubjectsAtTime = timetable
                                            .Where(t => t.days[i].Sum > j)
                                            .Select(t => new
                                            {
                                                subj = t.days[i].IndexedHours[j],
                                                clas = t.Class,
                                                idx = t.Class
                                            })
                                            .ToArray();

                    bool skip = false;
                    int ind = 0;
                    foreach (var subj in allSubjectsAtTime)
                    {
                        //
                        int k = timetable.IndexOf(timetable.Find(t => t.Class == subj.clas));
                        var subjType = allSubjectsAtTime[ind].subj.SubjectType;
                        var possibility = availableCabinets.FirstOrDefault(c => c.SubjectType == subjType);
                        if (subjType == SubjectType.Биология || subjType == SubjectType.Физика || subjType == SubjectType.Химия)
                        {
                            possibility = availableCabinets.FirstOrDefault(c => c.SubjectType == SubjectType.ПриродниНауки);
                        }
                        if (possibility == null)
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
                            indexes.Add(new Index(i, j, k, originalClassIndex,
                                                  repeatedHourGroupPosition, originalHourGroupPosition, busyDays));
                            skip = true;
                            break;
                        }
                        possibility.Class = allSubjectsAtTime[ind].clas;
                        CabinetsArrangment[i, j].Add(possibility);
                        availableCabinets.Remove(possibility);
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
                        var teach = repeats.FirstOrDefault(r => first[k].teacher.Any(t => t.Name == r.Split("/")[0]));
                        bool teacherIsBusy = first[k].teacher.Any(t => t.BusyDays.Any(b => b == (i + 1) * 10 + j + 1));

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
                                        busyDays.Add(b % 10);
                                    }
                                }
                            }
                            indexes.Add(new Index(i, j, first[k].indexOfClass, originalClassIndex,
                                                  repeatedHourGroupPosition, originalHourGroupPosition, busyDays));
                        }
                        else if ((indexes.Count == 0 && teach != null))
                        {
                            int originalClassIndex = int.Parse(teach.Split("/")[1]);
                            int repeatedHourGroupPosition = timetable[first[k].indexOfClass]
                                                            .days[i]
                                                            .Hours
                                                            .FindIndex(h => h.Subject == first[k].teacher[0].Subject);
                            int originalHourGroupPosition = timetable[int.Parse(teach.Split("/")[1])]
                                                            .days[i]
                                                            .Hours
                                                            .FindIndex(h => h.Teacher.Any(t => first[k].teacher.Any(k => k.Name == t.Name)));
                            indexes.Add(new Index(i, j, first[k].indexOfClass, originalClassIndex,
                                                  repeatedHourGroupPosition, originalHourGroupPosition));
                        }
                        else if (teach != null && !(indexes[^1].RepeatedClassIndex != first[k].indexOfClass &&
                                                    indexes[^1].DayOfTheWeek != i &&
                                                    indexes[^1].HourPositionInDay != j))
                        {
                            indexes.Add(new Index(i, j, first[k].indexOfClass, int.Parse(teach.Split("/")[1]),
                                                  timetable[first[k].indexOfClass].days[i].Hours
                                                                                  .FindIndex(h => h.Subject ==
                                                                                                  first[k].teacher[0]
                                                                                                          .Subject),
                                                  timetable[int.Parse(teach.Split("/")[1])].days[i].Hours
                                                                                           .FindIndex(
                                                                                               h => h.Teacher.Any(
                                                                                                   t => first[k].teacher
                                                                                                                .Any(
                                                                                                                    k =>
                                                                                                                        k.Name ==
                                                                                                                        t.Name)))));
                        }
                        else
                        {
                            repeats.AddRange(first[k].teacher.Select(r => r.Name)
                                                     .Select(t => t += "/" + first[k].indexOfClass));
                        }
                    }
                }
            }
        }

        static bool FindMistake(int i, int j, TeacherDTO[] teachers)
        {
            var first = timetable
                        .Where(t => t.days[i].Sum > j)
                        .Select(t => new
                        {
                            teacher = t.days[i].IndexedHours[j].Teacher,
                            indexOfClass = timetable.IndexOf(t)
                        }).ToList();
            bool isTeacherBusy = !teachers.Any(t => t.BusyDays.Any(i => i == (i + 1) * 10 + j + 1));
            return first.Count(f => f.teacher.Any(t => teachers.Any(r => r.Name == t.Name))) == 0 && isTeacherBusy;
        }

        public Tuple<List<TimeTable>, List<ClassroomDTO>[,]> Generate(List<ClassDTO> oldClasses, List<TeacherDTO> teachers, List<ClassroomDTO> cabinets)
        {
            void SwitchSubjects(string state)
            {
                FindMistakesALL();
                foreach (var index in indexes)
                {
                    var repeatedHoursList = timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek].Hours;
                    var originalHoursList = timetable[index.OriginalClassIndex].days[index.DayOfTheWeek].Hours;
                    SubjectDTO theHour = repeatedHoursList[index.RepeatedHourGroupPosition];
                    SubjectDTO first = originalHoursList[index.OriginalHourGroupPosition];
                    List<int> hourValids = new List<int>();
                    List<int> firstHourValids = new List<int>();
                    int pos = 0;
                    for (int i = 0; i < repeatedHoursList.Count; i++)
                    {
                        if (i == index.RepeatedHourGroupPosition)
                        {
                            pos += repeatedHoursList[i].TimesIn;
                            continue;
                        }
                        if (FindMistake(index.DayOfTheWeek, pos,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[index.HourPositionInDay]
                                                                           .Teacher)
                         && FindMistake(index.DayOfTheWeek, index.HourPositionInDay,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[pos].Teacher) &&
                            !index.Busies.Contains(i + 1))
                        {
                            hourValids.Add(i);
                        }

                        pos += repeatedHoursList[i].TimesIn;
                    }

                    pos = 0;
                    for (int i = 0; i < originalHoursList.Count; i++)
                    {
                        if (i == index.OriginalHourGroupPosition)
                        {
                            pos += originalHoursList[i].TimesIn;
                            continue;
                        }

                        index.OriginalHourGroupPosition = i;
                        if (FindMistake(index.DayOfTheWeek, pos,
                                        timetable[index.OriginalClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[index.HourPositionInDay]
                                                                           .Teacher)
                         && FindMistake(index.DayOfTheWeek, index.HourPositionInDay,
                                        timetable[index.RepeatedClassIndex].days[index.DayOfTheWeek]
                                                                           .IndexedHours[pos].Teacher) &&
                            !index.Busies.Contains(i + 1))
                        {
                            firstHourValids.Add(i);
                        }
                    }
                    //TODO: Why always 0? try random
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
                        if (state == "searching")
                        {
                            timetable[index.RepeatedClassIndex] = RandWeek(
                                teachers, oldClasses[index.RepeatedClassIndex]);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            //var subje = new Dictionary<string, int>();
            //foreach (var oldClass in oldClasses)
            //{
            //    foreach (var hour in oldClass.HourTimes)
            //    {
            //        if (subje.ContainsKey(hour.Key))
            //        {
            //            subje[hour.Key] += hour.Value;
            //        }
            //        else
            //        {
            //            subje.Add(hour.Key, hour.Value);
            //        }
            //    }
            //}

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    CabinetsArrangment[i, j] = new List<ClassroomDTO>();
                }
            }
            allCabinets = cabinets;
            int timesUntilReset = 80;
            int target = 20;
            int min = 100;
            do
            {
                timetable = new List<TimeTable>();
                indexes = new List<Index>();
                //INPUT
                List<ClassDTO> classses = new List<ClassDTO>(oldClasses);

                //GENERATE
                foreach (var clas in classses)
                {
                    timetable.Add(RandWeek(teachers, clas));
                }

                //MISSES
                int last = 0;
                int repeats = 0;
                FindMistakesALL();
                int t = 0;
                while (true)
                {
                    SwitchSubjects("searching");
                    t++;
                    if (indexes.Count < target)
                    {
                        break;
                    }

                    if (last == indexes.Count)
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
                    last = indexes.Count;
                    if (t == timesUntilReset)
                    {
                        break;
                    }
                }

                FindMistakesALL();
                min = Math.Min(min, indexes.Count);
            } while (indexes.Count >= target);
            return new Tuple<List<TimeTable>, List<ClassroomDTO>[,]>(timetable, CabinetsArrangment);
        }
    }
}
