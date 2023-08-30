using Microsoft.EntityFrameworkCore;
using VehicleProj.Models.Domain;

namespace VehicleProj.Data
{
    public class VehicleProjDbContext : DbContext
    {
        public VehicleProjDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
