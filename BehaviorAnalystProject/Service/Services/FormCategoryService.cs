using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class FormCategoryService
    {
        private readonly IRepository<FormCategory> repository;

        public FormCategoryService(IRepository<FormCategory> repository)
        {
            this.repository = repository;
        }

        public FormCategory AddItem(FormCategory item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot add a null item.");
            if (string.IsNullOrWhiteSpace(item.CategoryName))
                throw new ArgumentException("Form category name is required.");

            return repository.AddItem(item);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");
            var existing = repository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form category found with ID {id}.");

            repository.Delete(id);
        }

        public List<FormCategory> GetAll()
        {
            return repository.GetAll();
        }

        public FormCategory GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");
            var entity = repository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No form category found with ID {id}.");
            return entity;
        }

        public FormCategory GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid name.");
            var entity = repository.GetAll().FirstOrDefault(x => x.CategoryName == name);
            if (entity == null)
                throw new KeyNotFoundException($"No form category found with name {name}.");
            return entity;
        }

        public void UpdateItem(int id, FormCategory item)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot update a null item.");

            var existing = repository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form category found with ID {id}.");

            existing.CategoryName = item.CategoryName;
            repository.UpdateItem(id, existing);
        }
    }
}
