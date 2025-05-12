//using Microsoft.EntityFrameworkCore;
//using Repository.Entities;
//using Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Mock
//{
//    public class DataBase:DbContext,IContext
//    {
//        public DbSet<Analyst> Analyst { get; set; }
//        public DbSet<FormCategory> FormCategory { get; set; }
//        public DbSet<Child> Child { get; set; }
//        public DbSet<Comment> Comment { get; set; }
//        public DbSet<LessonSummary> LessonSummary { get; set; }
//        public DbSet<Form> Form { get; set; }

//        public void Save()
//        {
//            SaveChanges();
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("server = DESKTOP-1VUANBN; database = BehaviorAnalyst; trusted_connection=true; TrustServerCertificate=True");
//        }

//    }
//}
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;

public class DataBase : DbContext, IContext
{

    public DataBase(DbContextOptions<DataBase> options) : base(options)
    {
    }

    public DbSet<Analyst> Analyst { get; set; }
    public DbSet<FormCategory> FormCategory { get; set; }
    public DbSet<Child> Child { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<LessonSummary> LessonSummary { get; set; }
    public DbSet<Form> Form { get; set; }

    public void Save()
    {
        SaveChanges();
    }
}
