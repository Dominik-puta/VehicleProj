using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public interface IVehicleMakeService
    {
        public Task VehicleMakeAdd(AddVehicleMakeViewModel addVehicleMakeViewModel);
        Task VehicleMakeDelete(UpdateVehicleMakeViewModel model);
        Task VehicleMakeEditView(UpdateVehicleMakeViewModel model);
        Task<PaginatedList<IndexVehicleMakeViewModel>> VehicleMakeShowIndex(string sortOrder,string searchString, int? pageNumber, int pageSize);
        UpdateVehicleMakeViewModel VehicleMakeShowView(Guid id);
    }
}
