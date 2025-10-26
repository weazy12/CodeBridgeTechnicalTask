using System.Linq.Expressions;
using AutoMapper;
using FluentResults;
using MediatR;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Interfaces.Logging;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.QueryOptions;

namespace TechnicalTask.BLL.Mediatr.Dog.GetAll
{
    public class GetAllDogsHandler : IRequestHandler<GetAllDogsQuery, Result<List<DogDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public GetAllDogsHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }
        public async Task<Result<List<DogDto>>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = new QueryOptions<DAL.Entities.Dog>
            {
                Offset = (request.PageNumber - 1) * request.PageSize,
                Limit = request.PageSize
            };

            if (!string.IsNullOrEmpty(request.Attribute))
            {
                bool ascending = request.Order.ToLower() != "desc";
                var selector = GetSortSelector(request.Attribute);

                queryOptions.OrderByASC = ascending ? selector : null;
                queryOptions.OrderByDESC = ascending ? null : selector;
            }

            var dogs = await _repositoryWrapper.DogRepository.GetAllAsync(queryOptions);

            _loggerService.LogInformation("Success! Return all Dogs from db.");
            var dogsDto = _mapper.Map<List<DogDto>>(dogs.ToList());
            return Result.Ok(dogsDto);
        }

        private static Expression<Func<DAL.Entities.Dog, object>> GetSortSelector(string attribute) => attribute.ToLower() switch
        {
            "name" => d => d.Name,
            "color" => d => d.Color,
            "tail_length" => d => d.TailLength,
            "weight" => d => d.Weight,
            _ => d => d.Name
        };
    }
}
