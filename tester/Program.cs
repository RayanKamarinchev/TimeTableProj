using System;
using System.Collections.Generic;
using System.Reflection;
using Database;
using Database.DTOs;
using MyTimeTable.Models;
using TimeTableProj;
using MyTimeTable.Algorithm;

namespace tester
{
    internal class Program
    {
        static Controller helper = new Controller();
        static Generator gen = new Generator();
        static List<ClassDTO> classes;
        static List<TeacherDTO> teachers;
        static List<ClassroomDTO> cabinets;
        static void Main(string[] args)
        {
            classes = helper.GetClasses();
            teachers = helper.GetTeachers();
            cabinets = helper.GetClassrooms();
            var smt = gen.Generate(classes, teachers, cabinets);
            Console.WriteLine(smt);
        }
    }
}
