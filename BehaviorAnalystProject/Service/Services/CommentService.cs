using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class CommentService : IService<CommentDto>
    {
        private readonly IRepository<Comment> repository;
       // private readonly IMapper mapper;
        private readonly IRepository<Child> childRepository;
        private readonly MyMapper mapper = new MyMapper();

        public CommentService(IRepository<Comment> repository, IRepository<Child> childRepository, IMapper mapper)
        {
            this.repository = repository;
            this.childRepository = childRepository;
        }
        public CommentDto AddItem(CommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

           var child = childRepository.GetById(dto.ChildCode);
            if (child == null)
                throw new ArgumentException($"Child with code {dto.ChildCode} does not exist.");

            var comment = mapper.Map<CommentDto, Comment>(dto);
            child.ChildComments ??= new List<Comment>();
            child.ChildComments.Add(comment);

            comment.ChildCode = dto.ChildCode;
            var x = childRepository.UpdateItem(dto.ChildCode, child);

            return mapper.Map<Comment, CommentDto>(comment);
        }
      

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<CommentDto> GetAll()
        {
            var comments = repository.GetAll();
            return mapper.Map<List<Comment>, List<CommentDto>>(repository.GetAll());
        }

        public CommentDto GetById(int id)
        {
            var entity = repository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No comment found with ID {id}.");
            return mapper.Map<Comment,CommentDto>(entity);
        }

        public CommentDto UpdateItem(int id, CommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var existing = repository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No comment found with ID {id}.");

            var entity = mapper.Map<CommentDto, Comment>(dto);
            entity.ID = id; // נוודא שה-ID של הישות תואם למה שבא מה-URL

            var updated = repository.UpdateItem(id, entity);
            return mapper.Map<Comment,CommentDto>(updated);
        }


    }
}
