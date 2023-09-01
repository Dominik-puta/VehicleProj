using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;
using VehicleProj.Services;

namespace VehicleProj.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;
        private readonly IVehicleModelService vehicleModelService;


        public VehicleModelController(IMapper mapper, VehicleProjDbContext vehicleProjDbContext, IVehicleModelService _vehicleModelService)
        {
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
            this.vehicleModelService = _vehicleModelService;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            List<VehicleMake> vehicleMakes = vehicleModelService.ReturnVehicleMakeList();
            ViewData["vehicleMakes"] = vehicleMakes;
            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleModelViewModel addVehicleModelViewModel)
        {
            await vehicleModelService.VehicleModelAdd(addVehicleModelViewModel);
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
                                select s;
            IQueryable<IndexVehicleModelViewModel> models = _mapper.ProjectTo<IndexVehicleModelViewModel>(vehicleModels);
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(s => s.MakeName.ToUpper() == searchString.ToUpper());
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    models = models.OrderByDescending(s => s.MakeName);
                    break;
                case "Date":
                    models = models.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    models = models.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    models = models.OrderBy(s => s.MakeName);
                    break;
            }

            return View(await PaginatedList<IndexVehicleModelViewModel>.CreateAsync(models.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            List<VehicleMake> vehicleMakes = vehicleModelService.ReturnVehicleMakeList();
            ViewData["vehicleMakes"] = vehicleMakes;
            var viewModel = await vehicleModelService.VehicleModelShowView(id);
            if(viewModel != null)
            {
                return View("view" , viewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleModelViewModel model)
        {
            await vehicleModelService.VehicleModelEditView(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            Console.WriteLine(model.Name);
            await vehicleModelService.VehicleModelDelete(model);
            return RedirectToAction("Index");
        }
    }
}
