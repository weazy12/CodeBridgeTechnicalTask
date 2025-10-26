using FluentValidation;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Extentions;
using TechnicalTask.BLL.Resources;

namespace TechnicalTask.BLL.Validator.Dog
{
    public class BaseDogValidator : AbstractValidator<CreateDogDto>
    {
        public static readonly int MinNameAndColorLenght = 3;
        public static readonly int MaxNameAndColorLenght = 100;
        public BaseDogValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage(Errors_Dog_Validator.IsRequired.FormatWith("Name"))
                .MinimumLength(MinNameAndColorLenght)
                    .WithMessage(Errors_Dog_Validator.MinimumLenght.FormatWith("Name", MinNameAndColorLenght))
                .MaximumLength(MaxNameAndColorLenght)
                    .WithMessage(Errors_Dog_Validator.MaximumLenght.FormatWith("Name",MaxNameAndColorLenght));
            RuleFor(x => x.Color)
                .NotEmpty()
                    .WithMessage(Errors_Dog_Validator.IsRequired.FormatWith("Color"))
                .MinimumLength(MinNameAndColorLenght)
                    .WithMessage(Errors_Dog_Validator.MinimumLenght.FormatWith("Color", MinNameAndColorLenght))
                .MaximumLength(MaxNameAndColorLenght)
                    .WithMessage(Errors_Dog_Validator.MaximumLenght.FormatWith("Color", MaxNameAndColorLenght));

            RuleFor(x => x.TailLength)
                .NotNull()
                    .WithMessage(Errors_Dog_Validator.IsRequired.FormatWith("Tail length"))
                .GreaterThan(0)
                    .WithMessage(Errors_Dog_Validator.NotNegative.FormatWith("Tail length"));

            RuleFor(x => x.Weight)
                .NotNull()
                    .WithMessage(Errors_Dog_Validator.IsRequired.FormatWith("Weight"))
                .GreaterThan(0)
                    .WithMessage(Errors_Dog_Validator.NotNegative.FormatWith("Weight"));
        }
    }
}
