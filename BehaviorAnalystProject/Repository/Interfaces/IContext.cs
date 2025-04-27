using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContext
    {
        public DbSet<Analyst> Analyst { get; set; }
        public DbSet<FormCategory> FormCategory { get; set; }
        public DbSet<Child> Child { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<LessonSummary> LessonSummary { get; set; }
        public DbSet<Form> Form { get; set; }

        public void Save();
    }
}
