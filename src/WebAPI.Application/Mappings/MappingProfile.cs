using AutoMapper;
using WebAPI.Application.Features.Examples.DTOs;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Example, ExampleDTO>().ReverseMap();
        }
    }
}
