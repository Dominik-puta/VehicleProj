using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;


        public VehicleModelService(IMapper mapper, VehicleProjDbContext vehicleProjDbContext)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
        }

        public async Task VehicleModelAdd(AddVehicleModelViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
            var vehicleModel = _mapper.Map<AddVehicleModelViewModel, VehicleModel>(model);
            vehicleModel.MakeName = vehicleMake.Name;
            await vehicleProjDbContext.VehicleModels.AddAsync(vehicleModel);
            await vehicleProjDbContext.SaveChangesAsync();
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
