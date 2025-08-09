using FluentValidation;

namespace Autolux.CoreApp.Models.Cars;
public class CarCreateModelValidator : AbstractValidator<CarCreateModel>
{
    public CarCreateModelValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand must not be empty!");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must not be empty!");

        RuleFor(x => x.Year)
            .NotEmpty()
            .GreaterThanOrEqualTo(1900)
            .LessThanOrEqualTo(DateTime.Now.Year + 1)
            .WithMessage("The year must be between 1900 and the current date");

        RuleFor(x => x.Price)
            .GreaterThan(0.00)
            .LessThanOrEqualTo(99999999.99)
            .WithMessage("Price must be between 0.01 and 100 million");

        RuleFor(x => x.Miles)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(9999999)
            .WithMessage("Miles must be between 0 and 10 million");

        RuleFor(x => x.Transmission)
            .NotEmpty()
            .Must(value => value == "Manual" || value == "Automatic" || value == "EV" || value == "n/a")
            .WithMessage("Transmission must be one of the following:  Manual,  Automatic,  EV,  n/a");

        RuleFor(x => x.FuelType)
            .NotEmpty()
            .Must(value => value == "Petrol" || value == "Diesel" || value == "Electric" || value == "n/a")
            .WithMessage("FuelType must be one of the following:  Petrol,  Diesel,  Electric,  n/a");

        RuleFor(x => x.TankCapacity)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(1000)
            .WithMessage("TankCapacity must be between 0 and 1000");

        RuleFor(x => x.MilesPerGallon)
            .LessThanOrEqualTo(1000)
            .WithMessage("MilesPerGallon must be less than 1000");

        RuleFor(x => x.SeatCount)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("SeatCount must be between 1 and 100");

        RuleFor(x => x.DoorCount)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("DoorCount must be between 1 and 100");

        RuleFor(x => x.Colour)
            .NotEmpty()
            .WithMessage("Colour must not be empty");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty");
    }
}