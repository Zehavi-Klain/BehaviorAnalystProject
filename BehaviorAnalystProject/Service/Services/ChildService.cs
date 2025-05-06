using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ChildService : IService<ChildDto>
    {
        private readonly IRepository<Child> Repositery;
        private readonly IMapper mapper;

        public ChildService(IRepository<Child> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        public ChildDto AddItem(ChildDto item)
        {
            return mapper.Map<Child, ChildDto>(Repositery.AddItem(mapper.Map<ChildDto, Child>(item)));
        }
        public void Delete(int id)
        {
            Repositery.Delete(id);
        }

        public List<ChildDto> GetAll()
        {
            return mapper.Map<List<Child>, List<ChildDto>>(Repositery.GetAll());
        }

        public ChildDto GetById(int id)
        {
            return mapper.Map<Child, ChildDto>(Repositery.GetById(id));
        }

        public void UpdateItem(int id, ChildDto item)
        {
            Repositery.UpdateItem(id, mapper.Map<ChildDto, Child>(item));
        }

    }
}
