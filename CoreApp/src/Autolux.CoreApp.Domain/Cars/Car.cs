namespace Autolux.CoreApp.Domain.Cars;
public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; } = default!;
    public double Price { get; set; } = default!;

    public Car(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("Car Id cannot be empty");

        Id = id;
    }

    public Car(string brand, string name, int year, double price)
    {
        UpdateBrand(brand);
        UpdateName(name);
        UpdateYear(year);
        UpdatePrice(price);
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

}
