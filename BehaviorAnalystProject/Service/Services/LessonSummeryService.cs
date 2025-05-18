using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class LessonSummaryService : IService<LessonSummaryDto>
    {
        private readonly IRepository<LessonSummary> repository;
        private readonly IMapper mapper;
        private readonly IRepository<Child> childRepository;

        public LessonSummaryService(IRepository<LessonSummary> repository, IRepository<Child> childRepository, IMapper mapper)
        {
            this.repository = repository;
            this.childRepository = childRepository;
            this.mapper = mapper;
        }

        public LessonSummaryDto AddItem(LessonSummaryDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot add a null item.");

            var child = childRepository.GetById(item.ChildId);
            if (child == null)
                throw new ArgumentException($"Child with code {item.ChildId} does not exist.");

            var summery = mapper.Map<LessonSummaryDto,LessonSummary>(item);
            child.ChildLessonsSumery ??= new List<LessonSummary>();
            child.ChildLessonsSumery.Add(summery);

            summery.ChildId = item.ChildId;
            childRepository.UpdateItem(item.ChildId, child);

            return mapper.Map<LessonSummary,LessonSummaryDto>(summery);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var existing = repository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No lesson summary found with ID {id}.");

            repository.Delete(id);
        }

        public List<LessonSummaryDto> GetAll()
        {
            return mapper.Map<List<LessonSummary>, List<LessonSummaryDto>>(repository.GetAll());
        }

        public LessonSummaryDto GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var entity = repository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No lesson summary found with ID {id}.");

            return mapper.Map<LessonSummary,LessonSummaryDto>(entity);
        }

        public LessonSummaryDto UpdateItem(int id, LessonSummaryDto item)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot update a null item.");

            if (item.Date == default)
                throw new ArgumentException("Date is required.");

            if (string.IsNullOrWhiteSpace(item.Text))
                throw new ArgumentException("Text is required.");

            if (item.ChildId <= 0)
                throw new ArgumentException("ChildId must be a positive number.");

            var existing = repository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No lesson summary found with ID {id}.");

            var entityToUpdate = mapper.Map<LessonSummaryDto,LessonSummary>(item);
            entityToUpdate.ID = id;

            var updated = repository.UpdateItem(id, entityToUpdate);
            return mapper.Map<LessonSummaryDto>(updated);
        }
    }
}
