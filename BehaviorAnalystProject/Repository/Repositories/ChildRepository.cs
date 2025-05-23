using Microsoft.EntityFrameworkCore; // חשוב מאוד להוסיף את זה לטעינת קולקשנים
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class ChildRepository : IRepository<Child>
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
            var child = GetById(id);
            if (child == null)
                throw new KeyNotFoundException($"Child with ID {id} not found.");

            this.context.Child.Remove(child);
            context.Save();
        }

        public List<Child> GetAll()
        {
            return this.context.Child
                       .Include("ChildComments")
                       .Include("ChildForms")
                       .Include("ChildLessonsSumery")
                       .ToList();
        }

        public Child GetById(int id)
        {
            return context.Child.Include("ChildComments").Include("ChildForms").Include("ChildLessonsSumery").FirstOrDefault(x => x.Code == id);
        }

        public List<Comment> GetChildComments(int id)
        {
            var child = GetById(id);
            if (child == null)
                throw new ArgumentException($"Child with code {id} does not exist.");
            return child.ChildComments.ToList() ?? new List<Comment>();
        }

        public List<LessonSummary> GetChildLessonSummery(int id)
        {
            var child = GetById(id);
            if (child == null)
                throw new ArgumentException($"Child with code {id} does not exist.");
            return child.ChildLessonsSumery.ToList() ?? new List<LessonSummary>();
        }
        public List<Form> GetChildForms(int id)
        {
            var child = GetById(id);
            if (child == null)
                throw new ArgumentException($"Child with code {id} does not exist.");
            return child.ChildForms?.ToList() ?? new List<Form>();
        }

        public Child UpdateItem(int id, Child item)
        {
            var child = GetById(id);
            if (child == null)
                throw new KeyNotFoundException($"Child with ID {id} not found.");

            child.Email = item.Email;
            child.Address = item.Address;
            child.EducationalInstitution = item.EducationalInstitution;
            child.FamilyPosition = item.FamilyPosition;
            child.Fname = item.Fname;
            child.Lname = item.Lname;
            child.Birthday = item.Birthday;
            child.Gender = item.Gender;
            child.LessonNumber = item.LessonNumber;
            child.ChildsDisability = item.ChildsDisability;

            context.Save();
            return child;
        }
    }
}
