using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class FormService : IService<FormDto>
    {
        private readonly IRepository<Form> Repositery;
        private readonly IMapper mapper;

        public FormService(IRepository<Form> repositery, IMapper mapper)
        {
            Repositery = repositery ?? throw new ArgumentNullException(nameof(repositery), "Repository cannot be null.");
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null.");
        }

        public FormDto AddItem(FormDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot add a null item.");

            if (string.IsNullOrWhiteSpace(item.FileName))
                throw new ArgumentException("Form name is required.");

            Form entity = mapper.Map<FormDto, Form>(item);
            Form added = Repositery.AddItem(entity);
            return mapper.Map<Form, FormDto>(added);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            Repositery.Delete(id);
        }

        public List<FormDto> GetAll()
        {
            return mapper.Map<List<Form>, List<FormDto>>(Repositery.GetAll());
        }

        public FormDto GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var entity = Repositery.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            return mapper.Map<Form, FormDto>(entity);
        }

        public FormDto UpdateItem(int id, FormDto item)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot update a null item.");

            if (string.IsNullOrWhiteSpace(item.FileName))
                throw new ArgumentException("Form name is required.");

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            // מוודאים שה-ID של ה־DTO תואם לזה שב־URL
            var entityToUpdate = mapper.Map<Form>(item);
            entityToUpdate.Id = id;

            var updated = Repositery.UpdateItem(id, entityToUpdate);
            return mapper.Map<FormDto>(updated);
        }

    }
}
