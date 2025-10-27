using FluentAssertions;
using Moq;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Mediatr.Dog.Create;
using TechnicalTask.BLL.Validator.Dog;

namespace TechnicalTask.XunitTest.BLL.Validatior
{
    public class CreateDogValidatorTests
    {
        private readonly Mock<BaseDogValidator> _mockBaseDogValidator;
        private readonly CreateDogValidator _createDogValidator;
        public CreateDogValidatorTests()
        {
            _mockBaseDogValidator = new Mock<BaseDogValidator>();
            _createDogValidator = new CreateDogValidator(_mockBaseDogValidator.Object);
        }

        [Fact]
        public async Task ValidationPassSuccess_WhenCommandValid()
        {
            var command = CreateValidCommand();

            var result = await _createDogValidator.ValidateAsync(command);

            result.IsValid.Should().BeTrue();
        }

        private CreateDogCommand CreateValidCommand()
        {
            var dto = new CreateDogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 40,
                Weight = 70
            };

            return new CreateDogCommand(dto);
        }
    }
}
