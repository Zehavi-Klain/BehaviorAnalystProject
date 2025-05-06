using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FormRepository : IRepository<Form>
    {
        private readonly IContext context;

        public FormRepository(IContext context)
        {
            this.context = context;
        }

        public Form AddItem(Form item)
        {
            this.context.Form.Add(item);
            this.context.Save();
            return item;
        }

        public void Delete(int id)
        {
            this.context.Form.Remove(GetById(id));
            context.Save();
        }

        public List<Form> GetAll()
        {
            return this.context.Form.ToList();
        }

        public Form GetById(int id)
        {
            return this.context.Form.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Form item)
        {
            var form = GetById(id);
            form.FileName = item.FileName;
            form.FormCategory = item.FormCategory;
            form.FileUrl = item.FileUrl;
            context.Save();
        }
    }
}
