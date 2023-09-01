using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public interface IVehicleMakeService
    {
        public Task VehicleMakeAdd(AddVehicleMakeViewModel addVehicleMakeViewModel);
        Task VehicleMakeDelete(UpdateVehicleMakeViewModel model);
        Task VehicleMakeEditView(UpdateVehicleMakeViewModel model);
        UpdateVehicleMakeViewModel VehicleMakeShowView(Guid id);
    }
}
