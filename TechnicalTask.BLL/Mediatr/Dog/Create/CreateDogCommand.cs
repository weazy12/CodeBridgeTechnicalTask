using FluentResults;
using MediatR;
using TechnicalTask.BLL.DTOs.Dog;

namespace TechnicalTask.BLL.Mediatr.Dog.Create
{
    public record CreateDogCommand(CreateDogDto createDogDto) : IRequest<Result<DogDto>>;
}
