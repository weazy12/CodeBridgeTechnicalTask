using AutoMapper;
using TechnicalTask.BLL.DTOs.Dog;

namespace TechnicalTask.BLL.Mapping.Dog
{
    public class DogProfile : Profile
    {
        public DogProfile()
        {
            CreateMap<DAL.Entities.Dog, DogDto>().ReverseMap();
            CreateMap<CreateDogDto, DAL.Entities.Dog>();
        }
    }
}
