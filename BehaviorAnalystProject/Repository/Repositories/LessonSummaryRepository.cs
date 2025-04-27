using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LessonSummaryRepository : IRepository<LessonSummary>
    {
        private readonly IContext context;

        public LessonSummaryRepository(IContext context)
        {
            this.context = context;
        }

        public LessonSummary AddItem(LessonSummary item)
        {
            this.context.LessonSummary.Add(item);
            this.context.Save();
            return item;
        }

        public void Delete(int id)
        {
            this.context.LessonSummary.Remove(GetById(id));
            context.Save();
        }

        public List<LessonSummary> GetAll()
        {
            return this.context.LessonSummary.ToList();
        }

        public LessonSummary GetById(int id)
        {
            return this.context.LessonSummary.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateItem(int id, LessonSummary item)
        {
            var summary = GetById(id);
            summary.Text = item.Text;
            context.Save();
        }
    }
}
