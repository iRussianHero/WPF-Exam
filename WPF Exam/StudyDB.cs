using System;
using System.Data.Entity;
using System.Linq;

namespace WPF_Exam
{
    public class StudyDB : DbContext
    {

        public StudyDB()
            : base("name=StudyDB")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
    }
}