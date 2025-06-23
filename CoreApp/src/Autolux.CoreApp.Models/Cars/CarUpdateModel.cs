namespace Autolux.CoreApp.Models.Cars;
public record CarUpdateModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Brand { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; } = default!;
    public double Price { get; set; } = default!;
}
