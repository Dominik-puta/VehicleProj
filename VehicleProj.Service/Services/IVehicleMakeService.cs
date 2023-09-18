using VehicleProj.Service.Helpers;
using VehicleProj.Service.Models.Domain;
namespace VehicleProj.Service.Services
{
    public interface IVehicleMakeService
    {
        public Task VehicleMakeAdd(VehicleMake vehicleMake);
        Task VehicleMakeDelete(VehicleMake vehicleMake);
        Task VehicleMakeEditView(VehicleMake vehicleMake);
        Task<PaginatedList<VehicleMake>> VehicleMakeShowIndex(IndexArgs indexArgs);
        VehicleMake VehicleMakeShowView(Guid id);
    }
}
