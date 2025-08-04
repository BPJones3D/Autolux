using FluentValidation;

namespace Autolux.CoreApp.Models.Cars;
public class CarUpdateModelValidator : AbstractValidator<CarUpdateModel>
{
    public CarUpdateModelValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Brand).NotEmpty();
        RuleFor(x => x.Name).Empty();
        RuleFor(x => x.Year).NotEmpty();

        RuleFor(x => x.Price).GreaterThan(0.00).LessThan(99999999.99);
    }
}
