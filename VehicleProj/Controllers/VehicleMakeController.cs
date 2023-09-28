using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VehicleProj.MVC.Models;
using VehicleProj.Service.Models.Domain;
using VehicleProj.Service.Services;

namespace VehicleProj.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMakeService vehicleMakeService;


        public VehicleMakeController( IMapper mapper, IVehicleMakeService _vehicleMakeService)
        {
            this.vehicleMakeService = _vehicleMakeService;
            this._mapper = mapper;
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleMakeViewModel addVehicleMakeViewModel) 
        {

            VehicleMake vehicleMake = _mapper.Map<AddVehicleMakeViewModel, VehicleMake>(addVehicleMakeViewModel);
            await vehicleMakeService.AddAsync(vehicleMake);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public async  Task<ViewResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int? pageSize)
        {

            int defaSize = pageSize ?? 5;
            int defaPageNumber = pageNumber ?? 1;
            ViewData["PageSize"] = defaSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name desc" : "Name";
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
            PaginatedList<VehicleMake> models = await vehicleMakeService.ShowIndexAsync(new Service.Helpers.IndexArgs(sortOrder, searchString, defaPageNumber, defaSize));
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {

            VehicleMake vehicleMake =  await vehicleMakeService.ShowViewAsync(id);

            if (vehicleMake == null)
                Console.WriteLine("AAAA");
           
            if (vehicleMake != null)
            {
                UpdateVehicleMakeViewModel viewModel = _mapper.Map<VehicleMake, UpdateVehicleMakeViewModel>(vehicleMake);
                Response.StatusCode = 200;
                return View("View", viewModel);

            }
            else
            {
                Response.StatusCode = 404;
                ViewData["ErrorMessage"] = "There is no such ID \n" + id.ToString();
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleMakeViewModel model)
        {
            VehicleMake vehicleMake = _mapper.Map<UpdateVehicleMakeViewModel, VehicleMake>(model);
            await vehicleMakeService.EditAsync(vehicleMake);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleMakeViewModel model)
        {
            VehicleMake vehicleMake = _mapper.Map<UpdateVehicleMakeViewModel, VehicleMake>(model);
            await vehicleMakeService.DeleteAsync(vehicleMake);
            return RedirectToAction("Index");
        }
    }
}