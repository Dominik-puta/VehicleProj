using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using VehicleProj.Data;
using VehicleProj.Helpers;
using VehicleProj.Models;
using VehicleProj.Models.Domain;
using System.Linq.Dynamic.Core;

namespace VehicleProj.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;
        private readonly ISortHelper<VehicleMake> _sortHelper;
        private readonly IFilterHelper<VehicleMake> _filterHelper;



        public VehicleMakeService(IMapper mapper, VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleMake> sortHelper, IFilterHelper<VehicleMake> filterHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
            this._sortHelper = sortHelper;
            _filterHelper = filterHelper;
        }

        public async Task VehicleMakeAdd(AddVehicleMakeViewModel addVehicleMakeViewModel)
        {
            var vehicleMake = _mapper.Map<AddVehicleMakeViewModel, VehicleMake>(addVehicleMakeViewModel);
            await vehicleProjDbContext.VehicleMakes.AddAsync(vehicleMake);
            await vehicleProjDbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<IndexVehicleMakeViewModel>> VehicleMakeShowIndex(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            IQueryable<VehicleMake> vehicleMakes = from s in vehicleProjDbContext.VehicleMakes
                                                   select s;
            vehicleMakes = _filterHelper.ApplyFitler(vehicleMakes, searchString, "Name");
            vehicleMakes = _sortHelper.ApplySort(vehicleMakes, sortOrder);

            IQueryable<IndexVehicleMakeViewModel> models = _mapper.ProjectTo<IndexVehicleMakeViewModel>(vehicleMakes);
            return await PaginatedList<IndexVehicleMakeViewModel>.CreateAsync(models, pageNumber ?? 1, pageSize);
        }

        public UpdateVehicleMakeViewModel VehicleMakeShowView(Guid id)
        {
            var vehicleMake =  vehicleProjDbContext.VehicleMakes.FirstOrDefault(x => x.Id == id);
            if (vehicleMake != null)
            {
               var viewModel = _mapper.Map<VehicleMake, UpdateVehicleMakeViewModel>(vehicleMake);
               return viewModel;
            }
            return null;
        }
        public async Task VehicleMakeEditView(UpdateVehicleMakeViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                _mapper.Map(model,vehicleMake);
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }
        public async Task VehicleMakeDelete(UpdateVehicleMakeViewModel model)
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
