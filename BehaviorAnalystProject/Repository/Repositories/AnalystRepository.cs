using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AnalystRepository : IRepository<Analyst>
    {
        private readonly IContext context;
        public AnalystRepository(IContext context)
        {
            this.context = context;
        }
        public Analyst AddItem(Analyst item)
        {
            this.context.Analyst.Add(item);
            this.context.Save();
            return item;
        }

        public void Delete(int id)
        {
            this.context.Analyst.Remove(GetById(id));
            context.Save();
            Console.WriteLine("delted succsesfully");
        }

        public List<Analyst> GetAll()
        {
            return this.context.Analyst.ToList();

        }

        public Analyst GetById(int id)
        {
            return this.context.Analyst.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void UpdateItem(int id, Analyst item)
        {
            var analyst = GetById(id);
            analyst.Email = item.Email;
            analyst.Fname = item.Fname;
            analyst.Lname = item.Lname;
            context.Save();
        }
    }
}
