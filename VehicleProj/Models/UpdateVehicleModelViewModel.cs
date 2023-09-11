using VehicleProj.Service.Models.Domain;

namespace VehicleProj.MVC.Models
{
    public class UpdateVehicleModelViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public DateTime CreatedAt { get; set; }

        public VehicleMake Make { get; set; }

        public Guid MakeId { get; set; }
    }
}
