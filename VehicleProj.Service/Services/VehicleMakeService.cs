using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly ISortHelper<VehicleMake> _sortHelper;
        private readonly IFilterHelper<VehicleMake> _filterHelper;



        public VehicleMakeService( VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleMake> sortHelper, IFilterHelper<VehicleMake> filterHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._sortHelper = sortHelper;
            _filterHelper = filterHelper;
        }

        public async Task VehicleMakeAdd(VehicleMake vehicleMake)
        {
            await vehicleProjDbContext.VehicleMakes.AddAsync(vehicleMake);
            await vehicleProjDbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<VehicleMake>> VehicleMakeShowIndex(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            IQueryable<VehicleMake> vehicleMakes = from s in vehicleProjDbContext.VehicleMakes
                                                   select s;
            vehicleMakes = _filterHelper.ApplyFitler(vehicleMakes, searchString, "Name");
            vehicleMakes = _sortHelper.ApplySort(vehicleMakes, sortOrder);

            return await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes, pageNumber ?? 1, pageSize);
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
