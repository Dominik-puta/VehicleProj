using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VehicleProj.Data;
using VehicleProj.Models;
using VehicleProj.Models.Domain;


namespace VehicleProj.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly VehicleProjDbContext vehicleProjDbContext;
        private readonly IMapper _mapper;


        public VehicleMakeController( IMapper mapper,VehicleProjDbContext vehicleProjDbContext)
        {
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
            var vehicleMake = new VehicleMake() 
            {
                Id = Guid.NewGuid(),
                Name = addVehicleMakeViewModel.Name,
                Abrv = addVehicleMakeViewModel.Abrv,
                CreatedAt = DateTime.Now
            };
            await vehicleProjDbContext.VehicleMakes.AddAsync(vehicleMake);
            await vehicleProjDbContext.SaveChangesAsync();

            return RedirectToAction("Add");
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
            var vehicleMakes = from s in vehicleProjDbContext.VehicleMakes
                               select s;
            if(!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.ToUpper() == searchString.ToUpper());
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    vehicleMakes =  vehicleMakes.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    vehicleMakes = vehicleMakes.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                    break;
            }
            
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.AsNoTracking(), pageNumber ?? 1,pageSize ));
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleMake != null)
            {

               /* var viewModel = new UpdateVehicleMakeViewModel()
                {
                    Id = vehicleMake.Id,
                    Name = vehicleMake.Name,
                    Abrv = vehicleMake.Abrv
                };*/
               var viewModel = _mapper.Map<VehicleMake, UpdateVehicleMakeViewModel>(vehicleMake);
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateVehicleMakeViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                vehicleMake.Name = model.Name;
                vehicleMake.Abrv = model.Abrv;

                await vehicleProjDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleMakeViewModel model)
        {
            var vehicleMake = await vehicleProjDbContext.VehicleMakes.FindAsync(model.Id);
            if(vehicleMake != null)
            {
                vehicleProjDbContext.VehicleMakes.Remove(vehicleMake);
                await vehicleProjDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}