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
        private readonly MyMapper _mapper=new MyMapper();

        public ChildService(IRepository<Child> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        public ChildDto AddItem(ChildDto item)
        {
            var child = _mapper.Map<ChildDto, Child>(item);
            var result = Repositery.AddItem(child);
            return _mapper.Map<Child, ChildDto>(result);
        }
        public void Delete(int id)
        {
            Repositery.Delete(id);
        }

        public List<ChildDto> GetAll()
        {
            return _mapper.Map<List<Child>, List<ChildDto>>(Repositery.GetAll());
        }

        public ChildDto GetById(int id)
        {
            return _mapper.Map<Child, ChildDto>(Repositery.GetById(id));
        }

        public void UpdateItem(int id, ChildDto item)
        {
            Repositery.UpdateItem(id, _mapper.Map<ChildDto, Child>(item));
        }

    }
}
