using Microsoft.EntityFrameworkCore;
using VehicleProj.Data;
using VehicleProj.Helpers;
using VehicleProj.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VehicleProjDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleConnectionString")));
builder.Services.AddScoped<IVehicleMakeService,VehicleMakeService>();
builder.Services.AddScoped<IVehicleModelService,VehicleModelService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=VehicleMake}/{action=Index}/{id?}");

app.Run();
