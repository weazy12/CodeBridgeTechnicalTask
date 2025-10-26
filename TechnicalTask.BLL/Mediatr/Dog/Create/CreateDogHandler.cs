using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Extentions;
using TechnicalTask.BLL.Interfaces.Logging;
using TechnicalTask.BLL.Resources;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.QueryOptions;
using TechnicalTask.DAL.Repositories.Realizations.Base;

namespace TechnicalTask.BLL.Mediatr.Dog.Create
{
    public class CreateDogHandler : IRequestHandler<CreateDogCommand, Result<DogDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;
        public CreateDogHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }
        public async Task<Result<DogDto>> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            string errorMessage;
            var dogEntity = _mapper.Map<DAL.Entities.Dog>(request.createDogDto);

            var queryOptions = new QueryOptions<DAL.Entities.Dog>
            {
                Filter = d => d.Name == dogEntity.Name
            };
            var isDuplicate = await _repositoryWrapper.DogRepository.GetFirstOrDefaultAsync(queryOptions);

            
            if (isDuplicate != null)
            {
                errorMessage = Errors_DogErrros.IsDuplicate.FormatWith(dogEntity.Name);
                _loggerService.LogError(request,errorMessage);
                return Result.Fail<DogDto>(errorMessage);
            }

            await _repositoryWrapper.DogRepository.CreateAsync(dogEntity);

            if(await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                _loggerService.LogInformation($"Success! Dog was created!");
                var createdDogDto = _mapper.Map<DogDto>(dogEntity);
                return Result.Ok(createdDogDto);
            }

            errorMessage = Errors_DogErrros.FailedToCreate;
            _loggerService.LogError(request, errorMessage);
            return Result.Fail<DogDto>(errorMessage);
        }
    }
}
