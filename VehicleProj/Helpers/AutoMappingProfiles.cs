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
            CreateMap<VehicleMake, IndexVehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleMake, UpdateVehicleMakeViewModel>().ReverseMap();
            CreateMap<AddVehicleMakeViewModel, VehicleMake>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(u =>  Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(u =>  DateTime.Now));
            CreateMap<VehicleModel,UpdateVehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleModel, IndexVehicleModelViewModel>().ReverseMap();
            CreateMap<AddVehicleModelViewModel, VehicleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(u => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(u => DateTime.Now));
        }
    }
}
