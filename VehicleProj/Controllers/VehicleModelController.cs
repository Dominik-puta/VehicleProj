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
        public async Task<IActionResult> Add()
        {
            List<VehicleMake> vehicleMakes = await vehicleModelService.ReturnVehicleMakeListAsync();
            ViewData["vehicleMakes"] = vehicleMakes;
            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleModelViewModel addVehicleModelViewModel)
        {
            VehicleModel vehicleModel = _mapper.Map<AddVehicleModelViewModel, VehicleModel>(addVehicleModelViewModel);
            await vehicleModelService.AddAsync(vehicleModel);
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
            var models =  await vehicleModelService.ShowIndexAsync(new Service.Helpers.IndexArgs(sortOrder,searchString,defaPageNumber,defaSize));
            PaginatedList<IndexVehicleModelViewModel> viewModels = new PaginatedList<IndexVehicleModelViewModel>(
                _mapper.Map<List<VehicleModel>, List<IndexVehicleModelViewModel>>(models),
                models.TotalPages,
                models.PageIndex,
                defaSize);
            return View(viewModels);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            List<VehicleMake> vehicleMakes = await vehicleModelService.ReturnVehicleMakeListAsync();
            ViewData["vehicleMakes"] = vehicleMakes;
            var vehicleModel = await vehicleModelService.ShowViewAsync(id);
            UpdateVehicleModelViewModel viewModel = _mapper.Map<VehicleModel, UpdateVehicleModelViewModel>(vehicleModel);
            if(viewModel != null)
            {
                Response.StatusCode = 200;
                return View("view" , viewModel);
            }
            else
            {
                Response.StatusCode = 404;
                ViewData["ErrorMessage"] = "There is no such ID \n" + id.ToString();
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleModelViewModel model)
        {
            VehicleModel vehicleModel = _mapper.Map<UpdateVehicleModelViewModel, VehicleModel>(model);
            await vehicleModelService.EditAsync(vehicleModel);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            VehicleModel vehicleModel = _mapper.Map<UpdateVehicleModelViewModel, VehicleModel>(model);
            await vehicleModelService.DeleteAsync(vehicleModel);
            return RedirectToAction("Index");
        }
    }
}
