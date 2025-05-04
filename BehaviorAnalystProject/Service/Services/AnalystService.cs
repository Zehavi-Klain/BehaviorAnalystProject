using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using Repository.Interfaces;
using Repository.Entities;
using AutoMapper;  



namespace Service.Services
{
    public class AnalystService : IService<AnalystDto,string>
    {
        private readonly IRepository<Analyst,string> Repositery;
        private readonly IMapper mapper;

        public AnalystService(IRepository<Analyst,string> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        public AnalystDto AddItem(AnalystDto item)
        {
            return mapper.Map<Analyst, AnalystDto>(Repositery.AddItem(mapper.Map<AnalystDto, Analyst>(item)));
        }

        public void Delete(string id)
        {
            Repositery.Delete(id);
        }

        public List<AnalystDto> GetAll()
        {
            return mapper.Map<List<Analyst>,List<AnalystDto>>(Repositery.GetAll());
        }

        public AnalystDto GetById(string id)
        {
            return mapper.Map<Analyst,AnalystDto>(Repositery.GetById(id));
        }

        public void UpdateItem(string id, AnalystDto item)
        {
            Repositery.UpdateItem(id,mapper.Map<AnalystDto,Analyst>(item));
        }
    }
}
