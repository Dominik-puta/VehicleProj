using AutoMapper;
using Microsoft.EntityFrameworkCore.Update.Internal;
using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Helpers
{
    public class AutoMappingProfiles :Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<VehicleMake, UpdateVehicleMakeViewModel>().ReverseMap();
            //IndexMake
            CreateMap<VehicleModel,UpdateVehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleModel, IndexVehicleModelViewModel>().ReverseMap();
        }
    }
}
