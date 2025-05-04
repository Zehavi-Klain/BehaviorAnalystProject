using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FormCategoryRepository : IRepository<FormCategory, int>
    {
        private readonly IContext context;

        public FormCategoryRepository(IContext context)
        {
            this.context = context;
        }

        public FormCategory AddItem(FormCategory item)
        {
            this.context.FormCategory.Add(item);
            this.context.Save();
            return item;
        }

        public void Delete(int id)
        {
            this.context.FormCategory.Remove(GetById(id));
            context.Save();
        }

        public List<FormCategory> GetAll()
        {
            return this.context.FormCategory.ToList();
        }

        public FormCategory GetById(int id)
        {
            return this.context.FormCategory.FirstOrDefault(x => x.Code == id);
        }

        public void UpdateItem(int id, FormCategory item)
        {
            var category = GetById(id);
            category.CategoryName = item.CategoryName;
            context.Save();
        }
    }
}
