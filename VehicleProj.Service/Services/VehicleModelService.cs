using Microsoft.EntityFrameworkCore;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;
using VehicleProj.Service.Helpers;


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

        public async Task AddAsync(VehicleModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
           // model.MakeName = vehicleMake.Name;
            await vehicleProjDbContext.VehicleModels.AddAsync(model);
            await vehicleProjDbContext.SaveChangesAsync();
        }
        public async Task<PaginatedList<VehicleModel>> ShowIndexAsync(IndexArgs indexArgs)
        {
            IQueryable<VehicleModel> vehicleModels = vehicleProjDbContext.VehicleModels.Include(c => c.Make);
            vehicleModels = _filterHelper.ApplyFitler(vehicleModels, indexArgs.SearchString, "Make.Name");
            vehicleModels = _sortHelper.ApplySort(vehicleModels, indexArgs.SortOrder);
            PagingArgs<VehicleModel> pagingArgs = new PagingArgs<VehicleModel>(vehicleModels, indexArgs.PageNumber, indexArgs.PageSize);
            return await _pagingHelper.ApplyPaging(pagingArgs);
        }

        public async Task DeleteAsync(VehicleModel model)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);

            if (vehicleModel != null)
            {
                vehicleProjDbContext.VehicleModels.Remove(vehicleModel);
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task EditAsync(VehicleModel model)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            if (vehicleModel != null)
            {       
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task<VehicleModel> ShowViewAsync(Guid id)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleModel != null)
            {
                return vehicleModel;
            }
            return null;
        }
        public async Task<List<VehicleMake>> ReturnVehicleMakeListAsync()
        {
            return await vehicleProjDbContext.VehicleMakes.ToListAsync();
        }
    }
}
