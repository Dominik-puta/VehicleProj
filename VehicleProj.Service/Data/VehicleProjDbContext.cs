using Microsoft.EntityFrameworkCore;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Data
{
    public class VehicleProjDbContext : DbContext
    {
        public VehicleProjDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VehicleModel>()
                .HasOne(model => model.Make)
                .WithMany(make => make.VehicleModels)
                .HasForeignKey(model => model.MakeId)
                .IsRequired();
        }
    
    
    }
}
