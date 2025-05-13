using Common.Dto;
using Repository.Entities;
using System;
using System.IO;
using AutoMapper;

namespace Service.Services
{
    internal class MyMapper
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Forms/");
        private readonly IMapper _mapper;

        public MyMapper()
        {
            // יצירת קונפיגורציה של AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                // form
                cfg.CreateMap<Form, FormDto>()
                    .ForMember(dest => dest.ArrFile, opt => opt.MapFrom(src => File.ReadAllBytes(path + src.FileUrl)));
                cfg.CreateMap<FormDto, Form>()
                    .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => src.fileImage.FileName));

                // analyst
                cfg.CreateMap<AnalystDto, Analyst>().ReverseMap();

                // child
                // cfg.CreateMap<Child, ChildDto>().ReverseMap();


                // מ-Entity ל-DTO
                cfg.CreateMap<Child, ChildDto>()
                    .ForMember(dest => dest.AnalystId, opt => opt.MapFrom(src => src.AnalystCode));

                // מ-DTO ל-Entity
                cfg.CreateMap<ChildDto, Child>()
                    .ForMember(dest => dest.AnalystCode, opt => opt.MapFrom(src => src.AnalystId))
                    .ForMember(dest => dest.Analyst, opt => opt.Ignore()); // חשוב! לא לגעת ב-Analyst עצמו

            });

            // יצירת Mapper מתוך הקונפיגורציה
            _mapper = config.CreateMapper();
        }

        // דוגמה לשימוש ב-Mapper
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
