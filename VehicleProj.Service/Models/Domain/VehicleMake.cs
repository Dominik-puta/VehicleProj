namespace VehicleProj.Service.Models.Domain
{
    public class VehicleMake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
