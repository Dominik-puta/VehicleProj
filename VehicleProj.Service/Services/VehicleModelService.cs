using Microsoft.EntityFrameworkCore;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;
using VehicleProj.Service.Helpers;
using Microsoft.Data.SqlClient;

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
        public async Task<PaginatedList<VehicleModel>> VehicleModelShowIndex(IndexArgs indexArgs)
        {
            IQueryable<VehicleModel> vehicleModels = vehicleProjDbContext.VehicleModels.Include(c => c.Make);
            vehicleModels = _filterHelper.ApplyFitler(vehicleModels, indexArgs.searchString, "Make.Name");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine(indexArgs.sortOrder);
            vehicleModels = _sortHelper.ApplySort(vehicleModels, indexArgs.sortOrder);

            return await PaginatedList<VehicleModel>.CreateAsync(vehicleModels, indexArgs.pageNumber, indexArgs.pageSize);
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
