namespace VehicleProj.Service.Models.Domain
{
    public class VehicleModel
    {
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Abrv { get; set; }

        public string MakeName { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public Guid MakeId { get; set; }
    }
}
