using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;
using VehicleProj.Service.Helpers;

namespace VehicleProj.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly ISortHelper<VehicleMake> _sortHelper;
        private readonly IFilterHelper<VehicleMake> _filterHelper;
        private readonly IPagingHelper<VehicleMake> _pagingHelper;



        public VehicleMakeService( VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleMake> sortHelper, IFilterHelper<VehicleMake> filterHelper, IPagingHelper<VehicleMake> pagingHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._sortHelper = sortHelper;
            this._filterHelper = filterHelper;
            this._pagingHelper = pagingHelper;
        }

        public async Task VehicleMakeAdd(VehicleMake vehicleMake)
        {
            await vehicleProjDbContext.VehicleMakes.AddAsync(vehicleMake);
            await vehicleProjDbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<VehicleMake>> VehicleMakeShowIndex(IndexArgs indexArgs)
        {
            IQueryable<VehicleMake> vehicleMakes = from s in vehicleProjDbContext.VehicleMakes
                                                   select s;
            vehicleMakes = _filterHelper.ApplyFitler(vehicleMakes, indexArgs.searchString, "Name");
            vehicleMakes = _sortHelper.ApplySort(vehicleMakes, indexArgs.sortOrder);
            PagingArgs<VehicleMake> pagingArgs = new PagingArgs<VehicleMake>(vehicleMakes, indexArgs.pageNumber, indexArgs.pageSize);
            return await _pagingHelper.ApplyPaging(pagingArgs);
        }

        public VehicleMake VehicleMakeShowView(Guid id)
        {
            var vehicleMake =  vehicleProjDbContext.VehicleMakes.FirstOrDefault(x => x.Id == id);
            if (vehicleMake != null)
            {
               return vehicleMake;
            }
            return null;
        }
        public async Task VehicleMakeEditView(VehicleMake model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }
        public async Task VehicleMakeDelete(VehicleMake model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                vehicleProjDbContext.VehicleMakes.Remove(vehicleMake);
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }
    }
}
