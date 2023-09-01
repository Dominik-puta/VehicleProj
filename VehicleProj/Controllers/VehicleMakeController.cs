using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;
using VehicleProj.Services;

namespace VehicleProj.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;
        private readonly IVehicleMakeService vehicleMakeService;


        public VehicleMakeController( IMapper mapper,VehicleProjDbContext vehicleProjDbContext, IVehicleMakeService _vehicleMakeService)
        {
            this.vehicleMakeService = _vehicleMakeService;
            this.vehicleProjDbContext = vehicleProjDbContext;
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
            await vehicleMakeService.VehicleMakeAdd(addVehicleMakeViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public async  Task<ViewResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            //Number of items per page
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
            IQueryable<VehicleMake> vehicleMakes = from s in vehicleProjDbContext.VehicleMakes
                               select s;
            IQueryable<IndexVehicleMakeViewModel> models = _mapper.ProjectTo<IndexVehicleMakeViewModel>(vehicleMakes);
            Console.WriteLine(models.FirstOrDefault()?.Name);
            if(!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(s => s.Name.ToUpper() == searchString.ToUpper());
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    models = models.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    models = models.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    models = models.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    models = models.OrderBy(s => s.Name);
                    break;
            }
            
            return View(await PaginatedList<IndexVehicleMakeViewModel>.CreateAsync(models, pageNumber ?? 1,pageSize ));
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