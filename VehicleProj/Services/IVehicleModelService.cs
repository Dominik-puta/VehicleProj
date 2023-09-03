using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public interface IVehicleModelService
    {
        List<VehicleMake> ReturnVehicleMakeList();
        Task VehicleModelAdd(AddVehicleModelViewModel model);
        Task VehicleModelDelete(UpdateVehicleModelViewModel model);
        Task VehicleModelEditView(UpdateVehicleModelViewModel model);
        Task<PaginatedList<IndexVehicleModelViewModel>> VehicleModelShowIndex(string sortOrder, string searchString, int? pageNumber, int pageSize);
        Task<UpdateVehicleModelViewModel> VehicleModelShowView(Guid id);
    }
}
