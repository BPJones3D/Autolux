namespace Autolux.CoreApp.Models.Cars;
public record CarSummaryModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Brand { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; } = default!;
    public double Price { get; set; } = default!;
    public int Miles { get; set; } = default!;
    public string Transmission { get; set; } = default!;
    public string FuelType { get; set; } = default!;
    public double TankCapacity { get; set; } = default!;
    public double MilesPerGallon { get; set; } = default!;
    public int SeatCount { get; set; } = default!;
    public int DoorCount { get; set; } = default!;
    public string Colour { get; set; } = default!;
    public string Description { get; set; } = default!;
}
