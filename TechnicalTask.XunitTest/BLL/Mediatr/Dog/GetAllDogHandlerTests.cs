using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Interfaces.Logging;
using TechnicalTask.BLL.Mediatr.Dog.GetAll;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.Interfaces.Dog;
using TechnicalTask.DAL.Repositories.QueryOptions;

namespace TechnicalTask.XunitTest.BLL.Mediatr.Dog
{
    public class GetAllDogHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly Mock<ILoggerService> _loggerService;
        private readonly GetAllDogsHandler _handler;

        public GetAllDogHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _loggerService = new Mock<ILoggerService>();
            _handler = new GetAllDogsHandler(_mapper.Object, _repositoryWrapper.Object, _loggerService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllDogs_WithoutSortingAndPagination()
        {

            var dogEntities = new List<DAL.Entities.Dog>
            {
                new DAL.Entities.Dog { Id = 1, Name = "Buddy", Color = "Brown", TailLength = 15, Weight = 20 },
                new DAL.Entities.Dog { Id = 2, Name = "Max", Color = "Black", TailLength = 12, Weight = 18 }
            };

            var dogDtos = new List<DogDto>
            {
                new DogDto { Name = "Buddy", Color = "Brown", TailLength = 15, Weight = 20 },
                new DogDto { Name = "Max", Color = "Black", TailLength = 12, Weight = 18 }
            };

            _repositoryWrapper.Setup(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()))
                .ReturnsAsync(dogEntities);

            _mapper.Setup(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()))
                .Returns(dogDtos);


            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Buddy", result.Value[0].Name);
            Assert.Equal("Max", result.Value[1].Name);

            _repositoryWrapper.Verify(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mapper.Verify(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()), Times.Once);
            _loggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnDogsWithPagination()
        {
            var query = new GetAllDogsQuery
            {
                PageNumber = 1,
                PageSize = 5
            };

            QueryOptions<DAL.Entities.Dog>? capturedQueryOptions = null;

            _repositoryWrapper.Setup(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()))
                .Callback<QueryOptions<DAL.Entities.Dog>>(qo => capturedQueryOptions = qo)
                .ReturnsAsync(new List<DAL.Entities.Dog>());

            _mapper.Setup(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()))
                .Returns(new List<DogDto>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(capturedQueryOptions);
            Assert.Equal(0, capturedQueryOptions.Offset);
            Assert.Equal(5, capturedQueryOptions.Limit);
            _repositoryWrapper.Verify(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mapper.Verify(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()), Times.Once);
            _loggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldReturnDogsWhenSortingWork()
        {
            var query = new GetAllDogsQuery
            {
                Attribute = "name",
                Order = "asc"
            };

            QueryOptions<DAL.Entities.Dog>? capturedQueryOptions = null;

            _repositoryWrapper.Setup(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()))
                .Callback<QueryOptions<DAL.Entities.Dog>>(qo => capturedQueryOptions = qo)
                .ReturnsAsync(new List<DAL.Entities.Dog>());

            _mapper.Setup(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()))
                .Returns(new List<DogDto>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(capturedQueryOptions);
            Assert.NotNull(capturedQueryOptions.OrderByASC);
            _repositoryWrapper.Verify(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mapper.Verify(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()), Times.Once);
            _loggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnDogsWithPaginationAndSorting()
        {
            var query = new GetAllDogsQuery
            {
                PageNumber = 1,
                PageSize = 10,
                Attribute = "name",
                Order = "asc"
            };

            QueryOptions<DAL.Entities.Dog>? capturedQueryOptions = null;

            _repositoryWrapper.Setup(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()))
                .Callback<QueryOptions<DAL.Entities.Dog>>(qo => capturedQueryOptions = qo)
                .ReturnsAsync(new List<DAL.Entities.Dog>());

            _mapper.Setup(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()))
                .Returns(new List<DogDto>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(capturedQueryOptions);
            Assert.NotNull(capturedQueryOptions.OrderByASC);
            Assert.Equal(0, capturedQueryOptions.Offset);
            Assert.Equal(10, capturedQueryOptions.Limit);
            _repositoryWrapper.Verify(r => r.DogRepository.GetAllAsync(It.IsAny<QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mapper.Verify(m => m.Map<List<DogDto>>(It.IsAny<List<DAL.Entities.Dog>>()), Times.Once);
            _loggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
        }

    }

}
