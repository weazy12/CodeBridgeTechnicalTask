using FluentValidation;
using TechnicalTask.BLL.Mediatr.Dog.Create;

namespace TechnicalTask.BLL.Validator.Dog
{
    public class CreateDogValidator : AbstractValidator<CreateDogCommand>
    {
        public CreateDogValidator(BaseDogValidator baseDogValidator)
        {
            RuleFor(x => x.createDogDto).SetValidator(baseDogValidator);
        }
    }
}
