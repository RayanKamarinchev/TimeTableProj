using Database.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public partial class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options)
            :base(options)
        {
            
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<TeacherBusy> TeacherBusies { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<SubjectCell> Timetable { get; set; }
        public DbSet<ClassroomCell> ClassroomArrangement { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using (SqlConnection sqlDatabaseConnection = new SqlConnection("Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=Yes;"))
            {
                try
                {
                    sqlDatabaseConnection.Open();
                    string commandString = "ALTER DATABASE TimetableGen SET OFFLINE WITH ROLLBACK IMMEDIATE ALTER DATABASE TimetableGen SET SINGLE_USER EXEC sp_detach_db 'TimetableGen'";
                    SqlCommand sqlDatabaseCommand = new SqlCommand(commandString, sqlDatabaseConnection);
                    sqlDatabaseCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
            }
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassTeacher>(entity => { entity.HasKey(e => new { e.ClassId, e.TeacherId }); });
            modelBuilder.Entity<ClassSubject>(entity => { entity.HasKey(e => new { e.ClassId, e.SubjectId }); });
            modelBuilder.Entity<SubjectCellTeacher>(entity => { entity.HasKey(e => new { e.SubjectCellId, e.TeacherId }); });
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
