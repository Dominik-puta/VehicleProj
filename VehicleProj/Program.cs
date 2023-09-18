using Microsoft.EntityFrameworkCore;
using VehicleProj.Service.Data;
using VehicleProj.Helpers;
using VehicleProj.Service.Models.Domain;
using VehicleProj.Service.Services;
using VehicleProj.Service.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VehicleProjDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleConnectionString")));
builder.Services.AddScoped<IVehicleMakeService,VehicleMakeService>();
builder.Services.AddScoped<IVehicleModelService,VehicleModelService>();
builder.Services.AddScoped<ISortHelper<VehicleMake>, SortHelper<VehicleMake>>();
builder.Services.AddScoped<IFilterHelper<VehicleMake>, FilterHelper<VehicleMake>>();
builder.Services.AddScoped<IPagingHelper<VehicleMake>, PagingHelper<VehicleMake>>();
builder.Services.AddScoped<ISortHelper<VehicleModel>, SortHelper<VehicleModel>>();
builder.Services.AddScoped<IFilterHelper<VehicleModel>,FilterHelper<VehicleModel>>();
builder.Services.AddScoped<IPagingHelper<VehicleModel>, PagingHelper<VehicleModel>>();

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
