using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using TechnicalTask.BLL.DTOs.Dog;

namespace TechnicalTask.BLL.Mediatr.Dog.GetAll
{
    public record GetAllDogsQuery(int PageNumber = 1, int PageSize = 10, string? Attribute = null, string Order = "asc") : IRequest<Result<List<DogDto>>>;
}
