using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
