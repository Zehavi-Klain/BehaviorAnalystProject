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
    public class FormService : IService<FormDto, int>
    {
        private readonly IRepository<Form, int> Repositery;
        private readonly IMapper mapper;

        public FormService(IRepository<Form, int> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        public FormDto AddItem(FormDto item)
        {
            return mapper.Map<Form, FormDto>(Repositery.AddItem(mapper.Map<FormDto, Form>(item)));
        }

        public void Delete(int id)
        {
            Repositery.Delete(id);
        }

        public List<FormDto> GetAll()
        {
            return mapper.Map<List<Form>, List<FormDto>>(Repositery.GetAll());
        }

        public FormDto GetById(int id)
        {
            return mapper.Map<Form, FormDto>(Repositery.GetById(id));
        }

        public void UpdateItem(int id, FormDto item)
        {
            Repositery.UpdateItem(id, mapper.Map<FormDto, Form>(item));
        }
    }
}
