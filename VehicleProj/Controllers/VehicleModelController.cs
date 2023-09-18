using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using VehicleProj.Service.Models.Domain;
using VehicleProj.MVC.Models;
using VehicleProj.Service.Services;

namespace VehicleProj.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleModelService vehicleModelService;


        public VehicleModelController(IMapper mapper, IVehicleModelService _vehicleModelService)
        {
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
            VehicleModel vehicleModel = _mapper.Map<AddVehicleModelViewModel, VehicleModel>(addVehicleModelViewModel);
            await vehicleModelService.VehicleModelAdd(vehicleModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int? pageSize)
        {
            int defaSize = pageSize ?? 5;
            int defaPageNumber= pageNumber ?? 1;
            ViewData["PageSize"] = defaSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Make.Name" ? "Make.Name desc" : "Make.Name";
            ViewData["DateSortParm"] = sortOrder == "CreatedAt" ? "CreatedAt desc" : "CreatedAt";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var models =  await vehicleModelService.VehicleModelShowIndex(new Service.Helpers.IndexArgs(sortOrder,searchString,defaPageNumber,defaSize));
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            List<VehicleMake> vehicleMakes = vehicleModelService.ReturnVehicleMakeList();
            ViewData["vehicleMakes"] = vehicleMakes;
            var vehicleModel = await vehicleModelService.VehicleModelShowView(id);
            UpdateVehicleModelViewModel viewModel = _mapper.Map<VehicleModel, UpdateVehicleModelViewModel>(vehicleModel);
            if(viewModel != null)
            {
                return View("view" , viewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleModelViewModel model)
        {
            VehicleModel vehicleModel = _mapper.Map<UpdateVehicleModelViewModel, VehicleModel>(model);
            await vehicleModelService.VehicleModelEditView(vehicleModel);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            VehicleModel vehicleModel = _mapper.Map<UpdateVehicleModelViewModel, VehicleModel>(model);
            await vehicleModelService.VehicleModelDelete(vehicleModel);
            return RedirectToAction("Index");
        }
    }
}
