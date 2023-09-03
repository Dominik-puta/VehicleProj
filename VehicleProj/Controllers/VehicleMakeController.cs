using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;
using VehicleProj.Services;
using VehicleProj.Helpers;

namespace VehicleProj.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;
        private readonly IVehicleMakeService vehicleMakeService;
        private readonly ISortHelper<VehicleMake> _sortHelper;


        public VehicleMakeController( ISortHelper<VehicleMake> sortHelper, IMapper mapper,VehicleProjDbContext vehicleProjDbContext, IVehicleMakeService _vehicleMakeService)
        {
            this.vehicleMakeService = _vehicleMakeService;
            this.vehicleProjDbContext = vehicleProjDbContext;
            this._mapper = mapper;
            this._sortHelper = sortHelper;
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleMakeViewModel addVehicleMakeViewModel) 
        {
            await vehicleMakeService.VehicleMakeAdd(addVehicleMakeViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public async  Task<ViewResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            //Number of items per page
            //TODO custom pageSize trough browser
            int pageSize = 5;
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
            var models = await vehicleMakeService.VehicleMakeShowIndex(sortOrder, searchString ,pageNumber ,pageSize);
 
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            
                UpdateVehicleMakeViewModel viewModel =  vehicleMakeService.VehicleMakeShowView(id);
                if(viewModel != null)
                return  View("View", viewModel);
            else
            {
                Response.StatusCode = 404;
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleMakeViewModel model)
        {
            await vehicleMakeService.VehicleMakeEditView(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleMakeViewModel model)
        {
            await vehicleMakeService.VehicleMakeDelete(model);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}