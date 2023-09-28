using VehicleProj.Service.Helpers;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Services
{
    public interface IVehicleModelService
    {
        Task<List<VehicleMake>> ReturnVehicleMakeListAsync();
        Task AddAsync(VehicleModel model);
        Task DeleteAsync(VehicleModel model);
        Task EditAsync(VehicleModel model);
        Task<PaginatedList<VehicleModel>> ShowIndexAsync(IndexArgs indexArgs);
        Task<VehicleModel> ShowViewAsync(Guid id);
    }
}
