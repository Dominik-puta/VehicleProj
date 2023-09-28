using VehicleProj.Service.Helpers;
using VehicleProj.Service.Models.Domain;
namespace VehicleProj.Service.Services
{
    public interface IVehicleMakeService
    {
        Task AddAsync(VehicleMake vehicleMake);
        Task DeleteAsync(VehicleMake vehicleMake);
        Task EditAsync(VehicleMake vehicleMake);
        Task<PaginatedList<VehicleMake>> ShowIndexAsync(IndexArgs indexArgs);
        Task<VehicleMake> ShowViewAsync(Guid id);
    }
}
