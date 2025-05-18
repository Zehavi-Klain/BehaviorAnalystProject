using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ChildRepository:IRepository<Child>
    {
        private readonly IContext context;

        public ChildRepository(IContext context)
        {
            this.context = context;
        }

        public Child AddItem(Child item)
        {
            this.context.Child.Add(item);
            this.context.Save();
            return item;
        }

        public void Delete(int id)
        {
            this.context.Child.Remove(GetById(id));
            context.Save();
        }

        public List<Child> GetAll()
        {
            return this.context.Child.ToList();
        }

        public Child GetById(int id)
        {
            return this.context.Child.FirstOrDefault(x => x.Code==id);
        }
        public List<Comment> GetChildComments(int id)
        {
            var child = GetById(id);
            if (child == null)
                throw new ArgumentException($"Child with code {id} does not exist.");
            return child.ChildComments.ToList();
        }
        public List<Form> GetChildForms(int id)
        {
            var child = GetById(id);
            if (child == null)
                throw new ArgumentException($"Child with code {id} does not exist.");
            return child.ChildForms.ToList();
        }

        public Child UpdateItem(int id, Child item)
        {
            var child = GetById(id);
            child.Email = item.Email;
            child.Address = item.Address;
            child.EducationalInstitution = item.EducationalInstitution;
            child.FamilyPosition = item.FamilyPosition;
            child.Fname = item.Fname;
            child.Lname = item.Lname;
            child.ChildComments = item.ChildComments;
            context.Save();
            return child;
        }
    }
}

