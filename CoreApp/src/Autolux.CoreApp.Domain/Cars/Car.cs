namespace Autolux.CoreApp.Domain.Cars;
public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public Car(string brand, string name, int year, double price)
    {
        Brand = brand;
        Name = name;
        Year = year;
        Price = price;
    }
}
