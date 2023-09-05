using VehicleProj.Service.Models.Domain;
namespace VehicleProj.Service.Services
{
    public interface IVehicleMakeService
    {
        public Task VehicleMakeAdd(VehicleMake vehicleMake);
        Task VehicleMakeDelete(VehicleMake vehicleMake);
        Task VehicleMakeEditView(VehicleMake vehicleMake);
        Task<PaginatedList<VehicleMake>> VehicleMakeShowIndex(string sortOrder,string searchString, int? pageNumber, int pageSize);
        VehicleMake VehicleMakeShowView(Guid id);
    }
}
