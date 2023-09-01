using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;


        public VehicleMakeService(IMapper mapper, VehicleProjDbContext vehicleProjDbContext)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
        }

        public async Task VehicleMakeAdd(AddVehicleMakeViewModel addVehicleMakeViewModel)
        {
            var vehicleMake = _mapper.Map<AddVehicleMakeViewModel, VehicleMake>(addVehicleMakeViewModel);
            await vehicleProjDbContext.VehicleMakes.AddAsync(vehicleMake);
            await vehicleProjDbContext.SaveChangesAsync();
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
