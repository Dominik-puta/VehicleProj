using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;

namespace VehicleProj.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;


        public VehicleModelController(IMapper mapper, VehicleProjDbContext vehicleProjDbContext)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            List<VehicleMake> vehicleMakes = vehicleProjDbContext.VehicleMakes.ToList();
            ViewData["vehicleMakes"] = vehicleMakes;
            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleModelViewModel addVehicleModelViewModel)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(addVehicleModelViewModel.MakeId);
            var vehicleModel = new VehicleModel()
            {
                Id = Guid.NewGuid(),
                Name = addVehicleModelViewModel.Name,
                Abrv = addVehicleModelViewModel.Abrv,
                CreatedAt = DateTime.Now,
                MakeId = addVehicleModelViewModel.MakeId,
                MakeName = vehicleMake.Name
            };
            await vehicleProjDbContext.VehicleModels.AddAsync(vehicleModel);
            await vehicleProjDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            int pageSize = 5;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var vehicleModels = from s in vehicleProjDbContext.VehicleModels
                                select s; //   _mapper.Map<VehicleModel, IndexVehicleModelViewModel>(s);
                                

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(s => s.MakeName.ToUpper() == searchString.ToUpper());
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(s => s.MakeName);
                    break;
                case "Date":
                    vehicleModels = vehicleModels.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    vehicleModels = vehicleModels.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(s => s.MakeName);
                    break;
            }

            return View(await PaginatedList<VehicleModel>.CreateAsync(vehicleModels.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            List<VehicleMake> vehicleMakes = vehicleProjDbContext.VehicleMakes.ToList();
            ViewData["vehicleMakes"] = vehicleMakes;
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleModel != null)
            {

                /*var viewModel = new UpdateVehicleModelViewModel()
                {
                    Id = vehicleModel.Id,
                    Name = vehicleModel.Name,
                    Abrv = vehicleModel.Abrv

                };*/
                var viewModel = _mapper.Map<VehicleModel, UpdateVehicleModelViewModel>(vehicleModel);
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleModelViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.MakeId);
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            if (vehicleModel != null)
            {
                vehicleModel.Name = model.Name;
                vehicleModel.Abrv = model.Abrv;
                vehicleModel.MakeId = model.MakeId;
                vehicleModel.MakeName = vehicleMake.Name;
                await vehicleProjDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            var vehicleModel = await vehicleProjDbContext.VehicleModels.FindAsync(model.Id);
            if (vehicleModel != null)
            {
                vehicleProjDbContext.VehicleModels.Remove(vehicleModel);
                await vehicleProjDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
