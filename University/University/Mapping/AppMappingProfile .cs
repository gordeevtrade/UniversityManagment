using AutoMapper;
using University.BuisnessLogic.DTO;
using University.DAL;

namespace University.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Cours, CourseDTO>().ReverseMap();
            CreateMap<Groups, GroupDTO>().ReverseMap();
            CreateMap<Students, StudentDTO>().ReverseMap();
        }
    }
}
