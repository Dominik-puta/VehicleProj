using Microsoft.EntityFrameworkCore;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly ISortHelper<VehicleModel> _sortHelper;
        private readonly IFilterHelper<VehicleModel> _filterHelper;


        public VehicleModelService( VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleModel> sortHelper, IFilterHelper<VehicleModel> filterHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._sortHelper= sortHelper;
            this._filterHelper = filterHelper;
        }

        public async Task VehicleModelAdd(VehicleModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
           // model.MakeName = vehicleMake.Name;
            await vehicleProjDbContext.VehicleModels.AddAsync(model);
            await vehicleProjDbContext.SaveChangesAsync();
        }
        public async Task<PaginatedList<VehicleModel>> VehicleModelShowIndex(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            IQueryable<VehicleModel> vehicleModels = vehicleProjDbContext.VehicleModels.Include(c => c.Make);
            vehicleModels = _filterHelper.ApplyFitler(vehicleModels, searchString, "MakeName");
            vehicleModels = _sortHelper.ApplySort(vehicleModels, sortOrder);
            return await PaginatedList<VehicleModel>.CreateAsync(vehicleModels, pageNumber ?? 1, pageSize);
        }

        public async Task VehicleModelDelete(VehicleModel model)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);

            if (vehicleModel != null)
            {
                vehicleProjDbContext.VehicleModels.Remove(vehicleModel);
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task VehicleModelEditView(VehicleModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            if (vehicleModel != null)
            {
             //   vehicleModel.MakeName = vehicleMake.Name;        
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task<VehicleModel> VehicleModelShowView(Guid id)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleModel != null)
            {
                return vehicleModel;
            }
            return null;
        }
        public List<VehicleMake> ReturnVehicleMakeList()
        {
            return vehicleProjDbContext.VehicleMakes.ToList();
        }
    }
}
