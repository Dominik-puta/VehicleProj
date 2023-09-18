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
        private readonly IPagingHelper<VehicleModel> _pagingHelper;


        public VehicleModelService( VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleModel> sortHelper, IFilterHelper<VehicleModel> filterHelper, IPagingHelper<VehicleModel> pagingHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._sortHelper= sortHelper;
            this._filterHelper = filterHelper;
            this._pagingHelper = pagingHelper;
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
            vehicleModels = _sortHelper.ApplySort(vehicleModels, indexArgs.sortOrder);
            PagingArgs<VehicleModel> pagingArgs = new PagingArgs<VehicleModel>(vehicleModels, indexArgs.pageNumber, indexArgs.pageSize);
            return await _pagingHelper.ApplyPaging(pagingArgs);
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
