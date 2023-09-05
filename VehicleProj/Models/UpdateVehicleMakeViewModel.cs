namespace VehicleProj.MVC.Models
{
    public class UpdateVehicleMakeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
