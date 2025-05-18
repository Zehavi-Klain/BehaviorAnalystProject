using Common.Dto;
using Repository.Entities;
using System;
using System.IO;
using AutoMapper;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Forms/");
        private readonly IMapper _mapper;

        public MyMapper()
        {

            // form
            CreateMap<Form, FormDto>()
                .ForMember(dest => dest.ArrFile, opt => opt.MapFrom(src => File.ReadAllBytes(path + src.FileUrl)));
            CreateMap<FormDto, Form>()
                .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => src.fileImage.FileName));

            // analyst
            CreateMap<AnalystDto, Analyst>().ReverseMap();

            // comment
            CreateMap<Comment, CommentDto>().ReverseMap();

            // child
            CreateMap<Child, ChildDto>()
                .ForMember(dest => dest.AnalystId, opt => opt.MapFrom(src => src.AnalystCode));

            CreateMap<ChildDto, Child>()
                .ForMember(dest => dest.AnalystCode, opt => opt.MapFrom(src => src.AnalystId))
                .ForMember(dest => dest.Analyst, opt => opt.Ignore()); // חשוב! לא לגעת ב-Analyst עצמו
        }

    }
}
