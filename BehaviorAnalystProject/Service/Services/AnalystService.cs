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
    public class AnalystService : IService<AnalystDto>
    {
        private readonly IRepository<Analyst> Repositery;
        private readonly IMapper mapper;

        public AnalystService(IRepository<Analyst> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        //public AnalystDto AddItem(AnalystDto item)
        //{
        //    return mapper.Map<Analyst, AnalystDto>(Repositery.AddItem(mapper.Map<AnalystDto, Analyst>(item)));
        //}
        public AnalystDto AddItem(AnalystDto item)
        {
            // מיפוי מה-DTO לאובייקט Analyst
            var analyst = mapper.Map<AnalystDto, Analyst>(item);

            // הוספת ה-Analyst למאגר
            var addedAnalyst = Repositery.AddItem(analyst);

            // מיפוי מחדש מ-Analyst ל-AnalystDto כדי להחזיר את ה-DTO
            return mapper.Map<Analyst, AnalystDto>(addedAnalyst);
        }

        public void Delete(int id)
        {
            Repositery.Delete(id);
        }

        public List<AnalystDto> GetAll()
        {
            return mapper.Map<List<Analyst>,List<AnalystDto>>(Repositery.GetAll());
        }

        public AnalystDto GetById(int id)
        {
            return mapper.Map<Analyst,AnalystDto>(Repositery.GetById(id));
        }

        public void UpdateItem(int id, AnalystDto item)
        {
            Repositery.UpdateItem(id,mapper.Map<AnalystDto,Analyst>(item));
        }
    }
}
