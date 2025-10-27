using AutoMapper;
using FluentAssertions;
using Moq;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Interfaces.Logging;
using TechnicalTask.BLL.Mediatr.Dog.Create;
using TechnicalTask.BLL.Services.Logging;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.Realizations.Base;

namespace TechnicalTask.XunitTest.BLL.Mediatr.Dog
{
    public class CreateDogHandlerTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly CreateDogHandler _handler;
        public CreateDogHandlerTests()
        {

            _mockMapper = new Mock<IMapper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockLoggerService = new Mock<ILoggerService>();
            _handler = new CreateDogHandler(_mockMapper.Object, _mockRepositoryWrapper.Object, _mockLoggerService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenCreateSuccessfully()
        {
            var createDogDto = new CreateDogDto 
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20 
            };
            var dogEntity = new DAL.Entities.Dog 
            { 
                Id = 1,
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20 
            };
            var expectedDogDto = new DogDto 
            { 
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20 
            };

            _mockMapper.Setup(m => m.Map<DAL.Entities.Dog>(createDogDto)).Returns(dogEntity);

            _mockRepositoryWrapper.Setup(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()))
                .ReturnsAsync((DAL.Entities.Dog?)null);

            _mockRepositoryWrapper.Setup(r => r.DogRepository.CreateAsync(dogEntity)).ReturnsAsync(dogEntity);

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync())
               .ReturnsAsync(1);

            _mockMapper.Setup(m => m.Map<DogDto>(dogEntity)).Returns(expectedDogDto);

            var result = await _handler.Handle(new CreateDogCommand(createDogDto), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            _mockMapper.Verify(m => m.Map<DAL.Entities.Dog>(createDogDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.CreateAsync(It.IsAny<DAL.Entities.Dog>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<DogDto>(dogEntity), Times.Once);
            _mockLoggerService.Verify(l => l.LogError(It.IsAny<CreateDogCommand>(), It.IsAny<string>()), Times.Never);
            _mockLoggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenFindADuplicate()
        {
            var createDogDto = new CreateDogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };
            var dogEntity = new DAL.Entities.Dog
            {
                Id = 1,
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };
            var expectedDogDto = new DogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };

            _mockMapper.Setup(m => m.Map<DAL.Entities.Dog>(createDogDto)).Returns(dogEntity);

            _mockRepositoryWrapper.Setup(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()))
                .ReturnsAsync(dogEntity);

            var result = await _handler.Handle(new CreateDogCommand(createDogDto), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockMapper.Verify(m => m.Map<DAL.Entities.Dog>(createDogDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.CreateAsync(It.IsAny<DAL.Entities.Dog>()), Times.Never);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Never);
            _mockMapper.Verify(m => m.Map<DogDto>(dogEntity), Times.Never);
            _mockLoggerService.Verify(l => l.LogError(It.IsAny<CreateDogCommand>(), It.IsAny<string>()),Times.Once);
            _mockLoggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenCreateIsFailed()
        {
            var createDogDto = new CreateDogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };
            var dogEntity = new DAL.Entities.Dog
            {
                Id = 1,
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };
            var expectedDogDto = new DogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 20
            };

            _mockMapper.Setup(m => m.Map<DAL.Entities.Dog>(createDogDto)).Returns(dogEntity);

            _mockRepositoryWrapper.Setup(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()))
                .ReturnsAsync((DAL.Entities.Dog?)null);

            _mockRepositoryWrapper.Setup(r => r.DogRepository.CreateAsync(dogEntity)).ReturnsAsync(dogEntity);

            _mockRepositoryWrapper.Setup(r => r.SaveChangesAsync())
               .ReturnsAsync(0);

            var result = await _handler.Handle(new CreateDogCommand(createDogDto), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _mockMapper.Verify(m => m.Map<DAL.Entities.Dog>(createDogDto), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.GetFirstOrDefaultAsync(
                It.IsAny<DAL.Repositories.QueryOptions.QueryOptions<DAL.Entities.Dog>>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.DogRepository.CreateAsync(It.IsAny<DAL.Entities.Dog>()), Times.Once);
            _mockRepositoryWrapper.Verify(r => r.SaveChangesAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<DogDto>(dogEntity), Times.Never);
            _mockLoggerService.Verify(l => l.LogError(It.IsAny<CreateDogCommand>(), It.IsAny<string>()), Times.Once);
            _mockLoggerService.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Never);
        }
    }

}
