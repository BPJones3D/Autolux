namespace Autolux.CoreApp.Domain.Cars;
public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; } = default!;
    public double Price { get; set; } = default!;

    // car details
    public int Miles { get; set; } = default!;
    public string Transmission { get; set; } = default!;
    public string FuelType { get; set; } = default!;
    public double TankCapacity { get; set; } = default!;
    public double MilesPerGallon { get; set; } = default!;
    public int SeatCount { get; set; } = default!;
    public int DoorCount { get; set; } = default!;
    public string Colour { get; set; } = default!;
    public string Description { get; set; } = default!;


    public Car(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("Car Id cannot be empty");

        Id = id;
    }

    public Car(string brand, string name, int year, double price, int miles, string transmission, string fuelType, double tankCapacity,
        double MilesPerGallon, int seatCount, int doorCount, string colour, string description)
    {
        UpdateBrand(brand);
        UpdateName(name);
        UpdateYear(year);
        UpdatePrice(price);
        UpdateMiles(miles);
        UpdateTransmission(transmission);
        UpdateFuelType(fuelType);
        UpdateTankCapacity(tankCapacity);
        UpdateMilesPerGallon(MilesPerGallon);
        UpdateSeatCount(seatCount);
        UpdateDoorCount(doorCount);
        UpdateColour(colour);
        UpdateDescription(description);
    }

    public void UpdateBrand(string brand)
    {
        Brand = brand;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateYear(int year)
    {
        Year = year;
    }

    public void UpdatePrice(double price)
    {
        Price = price;
    }

    public void UpdateMiles(int miles)
    {
        Miles = miles;
    }
    public void UpdateTransmission(string transmission)
    {
        Transmission = transmission;
    }
    public void UpdateFuelType(string fuelType)
    {
        FuelType = fuelType;
    }
    public void UpdateTankCapacity(double tankCapacity)
    {
        TankCapacity = tankCapacity;
    }
    public void UpdateMilesPerGallon(double milesPerGallon)
    {
        MilesPerGallon = milesPerGallon;
    }
    public void UpdateSeatCount(int seatCount)
    {
        SeatCount = seatCount;
    }
    public void UpdateDoorCount(int doorCount)
    {
        DoorCount = doorCount;
    }
    public void UpdateColour(string colour)
    {
        Colour = colour;
    }
    public void UpdateDescription(string description)
    {
        Description = description;
    }


}
