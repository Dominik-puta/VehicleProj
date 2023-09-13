using VehicleProj.Service.Helpers;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Services
{
    public interface IVehicleModelService
    {
        List<VehicleMake> ReturnVehicleMakeList();
        Task VehicleModelAdd(VehicleModel model);
        Task VehicleModelDelete(VehicleModel model);
        Task VehicleModelEditView(VehicleModel model);
        Task<PaginatedList<VehicleModel>> VehicleModelShowIndex(IndexArgs indexArgs);
        Task<VehicleModel> VehicleModelShowView(Guid id);
    }
}
