using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using VehicleProj.Data;
using VehicleProj.Helpers;
using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;
        private readonly ISortHelper<VehicleModel> _sortHelper;
        private readonly IFilterHelper<VehicleModel> _filterHelper;


        public VehicleModelService(IMapper mapper, VehicleProjDbContext vehicleProjDbContext, ISortHelper<VehicleModel> sortHelper, IFilterHelper<VehicleModel> filterHelper)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
            this._sortHelper= sortHelper;
            this._filterHelper = filterHelper;
        }

        public async Task VehicleModelAdd(AddVehicleModelViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
            var vehicleModel = _mapper.Map<AddVehicleModelViewModel, VehicleModel>(model);
            vehicleModel.MakeName = vehicleMake.Name;
            await vehicleProjDbContext.VehicleModels.AddAsync(vehicleModel);
            await vehicleProjDbContext.SaveChangesAsync();
        }
        public async Task<PaginatedList<IndexVehicleModelViewModel>> VehicleModelShowIndex(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            IQueryable<VehicleModel> vehicleModels = from s in vehicleProjDbContext.VehicleModels
                                                   select s;
            vehicleModels = _filterHelper.ApplyFitler(vehicleModels, searchString, "MakeName");
            vehicleModels = _sortHelper.ApplySort(vehicleModels, sortOrder);
            IQueryable<IndexVehicleModelViewModel> models = _mapper.ProjectTo<IndexVehicleModelViewModel>(vehicleModels);
            return await PaginatedList<IndexVehicleModelViewModel>.CreateAsync(models, pageNumber ?? 1, pageSize);
        }

        public async Task VehicleModelDelete(UpdateVehicleModelViewModel model)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            Console.WriteLine(vehicleModel.Name);
            if (vehicleModel != null)
            {
                Console.WriteLine(vehicleModel.Name);
                vehicleProjDbContext.VehicleModels.Remove(vehicleModel);
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task VehicleModelEditView(UpdateVehicleModelViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            if (vehicleModel != null)
            {
                _mapper.Map(model, vehicleModel);
                vehicleModel.MakeName = vehicleMake.Name;        
                await vehicleProjDbContext.SaveChangesAsync();
            }
        }

        public async Task<UpdateVehicleModelViewModel> VehicleModelShowView(Guid id)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleModel != null)
            {
                var viewModel = _mapper.Map<VehicleModel, UpdateVehicleModelViewModel>(vehicleModel);
                return viewModel;
            }
            return null;
        }
        public List<VehicleMake> ReturnVehicleMakeList()
        {
            return vehicleProjDbContext.VehicleMakes.ToList();
        }
    }
}
