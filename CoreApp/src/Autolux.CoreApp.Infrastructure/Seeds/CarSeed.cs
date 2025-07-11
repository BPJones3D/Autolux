using Autolux.CoreApp.Domain.Cars;

namespace Autolux.CoreApp.Infrastructure.Seeds;
public static class CarSeed
{
    public static List<Car> GetCars()
    {
        var awardRules = new List<Car>
        {
            new("BMW", "iX", 2025, 78000, 82371, "EV", "Electric", 383, 0, 5, 5, "Blue", string.Empty),
            new("BMW", "X2", 2024, 42000, 28173, "Automatic", "Petrol", 54, 28, 5, 5, "Grey", string.Empty),
            new("Ford", "Fiesta", 2024, 23000, 35211, "Automatic", "Petrol", 41, 59, 5, 5, "White", string.Empty),
            new("Ford", "Focus ST", 2020, 34857, 34827, "Manual", "Petrol", 52, 35.3, 5, 5, "Red", string.Empty),
            new("Ford", "Mustang",  2025, 55000, 25135, "Automatic", "Petrol", 61, 30, 4, 2, "White", string.Empty),
            new("Honda", "Civic Type R", 2018, 22100, 41723, "Manual", "Petrol", 47, 34.4, 5, 5, "White", string.Empty),
            new("Jaguar", "XE R-Dynamic S", 2023, 28200, 16273, "Automatic", "Petrol", 63, 25, 5,  5, "Black", string.Empty),
            new("Land Rover", "Defender 130", 2025, 84070, 71820, "Automatic", "Petrol", 90, 24, 7,  5, "Charcoal", string.Empty),
            new("Land Rover", "Range Rover Evoque", 2021, 28590, 26437, "Automatic", "Petrol", 67, 22, 5,  5, "White", string.Empty),
            new("MG", "MG4 EV Trophy Long Range", 2023, 26500, 27381, "EV", "Electric", 225, 0, 5, 5, "Red", string.Empty),
            new("Mini", "Cooper S", 2022, 19500, 82738, "Automatic", "Petrol", 44, 30, 4, 3, "Grey", string.Empty),
            new("Nissan", "Leaf",  2022, 33000, 12421, "EV", "Electric", 375, 0, 5, 5, "White", string.Empty),
            new("Nissan", "Qashqai Tekna+", 2024, 29500, 9182, "Automatic", "Petrol", 55, 44, 5, 5, "Blue", string.Empty),
            new("Subaru", "Outback",  2025, 37000, 15235, "Automatic", "Petrol", 60, 26, 5, 5, "Grey", string.Empty),
            new("Vauxhall", "Astra GTC", 2015, 7585, 73827, "Manual", "Diesel", 56, 39.2, 5, 3, "Blue", string.Empty),
            new("Vauxhall", "Corsa‑e", 2025, 31500, 32131, "EV", "Electric", 209, 0, 5, 5, "Grey", string.Empty),
            new("Vauxhall", "Grandland X", 2024, 33000, 43252, "Manual", "Diesel", 53, 68, 5, 5, "Black", string.Empty),
            new("Vauxhall", "Mokka‑e", 2025, 34000, 54321, "EV", "Electric", 209, 0, 5,  5, "Blue", string.Empty)
        };

        return awardRules;
    }
}
