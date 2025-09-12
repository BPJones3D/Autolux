using Autolux.CoreApp.Models.Cars;

namespace Autolux.CoreApp.Api.Entities;
public interface ICarService
{
    Task<CarModel> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken);
    Task DeleteAsync(CarDeleteModel carDeleteModel, CancellationToken cancellationToken);
    Task<CarModel> UpdateAsync(CarUpdateModel carUpdateModel, CancellationToken cancellationToken);
    Task<CarModel> GetByIdAsync(Guid carId, CancellationToken cancellationToken);
    Task<List<CarSummaryModel>> GetSummaryListAsync(string filter, string filterOrder, CancellationToken cancellationToken);
    Task<List<CarModel>> GetListByBrandAsync(string brand, CancellationToken cancellationToken);
    Task<List<CarModel>> GetListByYearAsync(int year, CancellationToken cancellationToken);
    Task<List<CarModel>> GetListByPriceBracketAsync(double low, double high, CancellationToken cancellationToken);
}
