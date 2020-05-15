using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VMS.Data.Models;
using VMS.Data.Services;
using VMS.Web.ViewModels;

namespace VMS.Web.Controllers
{
    public class ServiceController : BaseController
    {
        //Constructor
        private readonly VehicleDbService svc;
        public ServiceController()
        {
            svc = new VehicleDbService();
        }
        
        // GET /service/index
        public IActionResult Index(string sortOrder)
        {
           //if string is null or empty define a sort order
           ViewBag.DateSortParm = String.IsNullOrEmpty (sortOrder) ? "date_desc" :" ";
           //Make reg number another attribute to sort for incase the vehicle has multiple services
           ViewBag.RegSortParm = sortOrder == "Reg" ? "reg_desc" : "Reg";

           //load the services
           var services = svc.GetAllServiceRecords();

           //Create a switch statement to order the attributes
           switch (sortOrder)
                {
                    case "date_desc":
                        services=services.OrderByDescending (s => s.DateofService).ToList();
                    break;
                    case "reg_desc":
                        services=services.OrderByDescending (s => s.RegNumber).ToList();
                    break;
                    //default will be the Id 
                    default:
                        services=services.OrderBy (s => s.Id).ToList();
                    break;
                }    
                    //return the service view page
                    return View (services);

                }

        // GET /service/create
        public IActionResult Create()
        {
            var vehicles = svc.GetAllVehicles();
            var serviceVM = new ServiceViewModel
            {
                Vehicles = new SelectList(vehicles,"Id","RegistrationNumber")
            };
            
            return View(serviceVM);
        }
        
        //POST /service/create
        [HttpPost]
        public IActionResult Create(ServiceViewModel serviceVM)
        {
            if (ModelState.IsValid)
            {
               var s =  new Service
                {
                    VehicleId = serviceVM.VehicleId,
                    ServiceProvided = serviceVM.ServiceProvided,
                    StaffMember = serviceVM.StaffMember,
                    DateofService= serviceVM.DateofService,
                    Cost =  serviceVM.Cost,
                    Mileage = serviceVM.Mileage
                    
                };
               svc.AddService(s);
                
                Alert ("Service Added", AlertType.info);
                return RedirectToAction(nameof(Index));
            }
            // redisplay the form for editing
            return View(serviceVM);
        }

        //GET / Service/Delete
        public IActionResult Delete(int id)
        {
            // load service   
            var service = svc.GetServiceById(id);
            
            if (service == null)
            {
                Alert ("Service not found" , AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
        
            // pass service to view to confirm delete
            return View(service);
        }
        
        
        //POST /Service/Delete
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            //Retrieve the sevice for the vehicle and delete it
           var service=svc.GetServiceById(id);
           svc.DeleteService(id);

            //add alert to state service has been deleted successfully
            Alert ("Service has been deleted", AlertType.success);

            //redirect back to the vehicle details that match and not the service index page
           
            return RedirectToAction("Details", "Vehicle", new { id = service.VehicleId});
        }

    }
}