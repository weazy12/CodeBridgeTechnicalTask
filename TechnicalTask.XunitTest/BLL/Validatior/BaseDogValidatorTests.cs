using FluentAssertions;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Validator.Dog;

namespace TechnicalTask.XunitTest.BLL.Validatior
{
    public class BaseDogValidatorTests
    {
        private readonly BaseDogValidator _baseDogValidator;
        public BaseDogValidatorTests()
        {
            _baseDogValidator = new BaseDogValidator();
        }

        [Fact]
        public async Task ShouldPass_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidationFail_WhenNameIsEmpty()
        {
            var dto = CreateValidDto();
            dto.Name = string.Empty;   

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatiorFail_WhenNameTooShort()
        {
            var dto = CreateValidDto();

            dto.Name = "a";

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatiorFail_WhenNameToolong()
        {

            var dto = CreateValidDto();
            dto.Name = new string('a', BaseDogValidator.MaxNameAndColorLenght + 1);
            
            var result = await _baseDogValidator.ValidateAsync(dto);
            
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidationFail_WhenColorIsEmpty()
        {
            var dto = CreateValidDto();
            dto.Color = string.Empty;

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatiorFail_WhenColorTooShort()
        {
            var dto = CreateValidDto();

            dto.Color = "a";

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidatiorFail_WhenColorToolong()
        {

            var dto = CreateValidDto();
            dto.Color = new string('a', BaseDogValidator.MaxNameAndColorLenght + 1);

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidationFail_WhenTailLenghtIsNull()
        {
            var dto = CreateValidDto();
            dto.TailLength = default;

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidationFail_WhenTailLenghtNegative()
        {
            var dto = CreateValidDto();
            dto.TailLength = -1;

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidationFail_WhenWeightIsNull()
        {
            var dto = CreateValidDto();
            dto.Weight = default;

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidationFail_WhenWeightNegative()
        {
            var dto = CreateValidDto();
            dto.Weight = -1;

            var result = await _baseDogValidator.ValidateAsync(dto);

            result.IsValid.Should().BeFalse();
        }
        private CreateDogDto CreateValidDto()
        {
            return new CreateDogDto
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 40,
                Weight = 70
            };
        }
    }
}
