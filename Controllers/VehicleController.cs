using System;
using System.Linq;
using VMS.Data.Models;
using VMS.Data.Services;
using VMS.Web.ViewModels;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace VMS.Web.Controllers
{

 public class VehicleController : BaseController
    {
            //Constructor
             private readonly VehicleDbService svc;

            public VehicleController()
            {
                svc = new VehicleDbService();
            }
            // GET /Vehicle/index
    
            // GET /Vehicle/details/{id}


            // To sort order  create a ViewData with the parameters defined (Make, DateOfReg, Fuel Type)
            //This code receives a sortOrder parameter from the query string in the URL
            
            public IActionResult Index(string sortOrder)
        {
             //if string is null or empty define a sort order
             ViewBag.MakeSortParm =  String.IsNullOrEmpty(sortOrder) ? "make_desc": "";
             
             ViewBag.FuelSortParm = sortOrder == "Fuel" ? "fuel_desc" : "Fuel";
             ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            
             //load the vehicles
             var vehicles = svc.GetAllVehicles();

             //Create a switch statement to order the attributes
             switch (sortOrder)
                 {
                      case "make_desc":
                         vehicles = vehicles.OrderByDescending(v => v.Make).ToList();
                      break;
                      case "fuel_desc":
                        vehicles = vehicles.OrderByDescending(v => v.FuelType).ToList();
                      break;
                      case "date_desc":
                         vehicles = vehicles.OrderByDescending(v => v.DateOfRegistration).ToList();
                      break;
                      //Default will be to list by Vehicle Id(Page will open with this order)
                      default:
                         vehicles = vehicles.OrderBy(v => v.Id).ToList();
                      break;
         }
                    //Return the vehicle view page
                     return View(vehicles);
        }   
        public IActionResult Details(int id)
        {
            //check if vehicle Id exists and an alert and the index page again 
            //if it does it will display the vehicle details
            var vehicle = svc.GetVehicleById(id);
            if (vehicle == null)
            {
                Alert ("Vehicle not found" , AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);
        }
           //GET/Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

            //POST/Vehicle/Create
            [HttpPost]
            public IActionResult Create (Vehicle v)
            {
                if (ModelState.IsValid)
                    //If two vehicles have the same reg then add vehicle will return null
                {
                    if (svc.AddVehicle(v) != null)
                
                {
                        //Add the vehicle to the database
                        svc.AddVehicle(v);
                        //Add an alert if it is created successfully
                        Alert("Vehicle Added", AlertType.info);
                        return RedirectToAction (nameof(Index));
                }

                    //If null is returned then we redisplay form with error message
                        Alert ("Vehicle Not Added As Duplicate Reg Number Entered", AlertType.danger);
                        return View(v);
            }
                //redisplay the form for editing
                return View(v);

                 }
             //GET/Vehicle/Edit
            public IActionResult Edit(int id)
            {
                //Load the vehicle using the get vehicle method 
                
                var v = svc.GetVehicleById(id);

                //If no vehicle found return not found
                if (v == null)
                {
                    Alert ("Vehicle not found" , AlertType.warning);
                     return RedirectToAction(nameof(Index));
                }
                
                //send the vehicle to the view to edit
                return View(v);
            }

             //POST/Vehicle/Edit
             //update the vehicles details
            [HttpPost]
            public IActionResult Edit(int id, Vehicle v)
            {
                //Verify model is valid
                if (ModelState.IsValid)
                {
                    //if validated call the update vehicle method and redirect action back to index
                    svc.UpdateVehicle(id, v);
                    //add info alert to show vehicle has been updated
                    Alert("Vehicle has been updated", AlertType.info);
                    return RedirectToAction(nameof(Index));
                }
                //redisplay the form for editing
                return View(v);
            }


            //GET /Vehicle/Delete/{id}
           public IActionResult Delete(int id)
            {   
            // load vehicle via service         
            var vehicle = svc.GetVehicleById(id);
            if (vehicle == null)
            {
                 Alert("Vehicle Not Found", AlertType.warning);
                return NotFound();
               
            }
            // pass vehicle to view to confirm delete
            return View(vehicle);
            }

             //POST/Vehicle/Delete
             [HttpPost]
            public IActionResult DeleteConfirm(int id)
            {
                //delete the vehicle selected
                svc.DeleteVehicle(id);
                //add alert to state vehicle has been deleted successfully
                Alert ("Vehicle has been deleted", AlertType.success);
                //redirect back to the vehicle index
                return RedirectToAction (nameof(Index));
            }

            //Service methods
            //GET/Service/Create
            public IActionResult AddService (int id)
            {
                //add the vehicle and return not found if it does not exist
                var vehicle = svc.GetVehicleById(id);
                if (vehicle == null)
                {
                    Alert ("Vehicle not found" , AlertType.warning);
                    return RedirectToAction(nameof(Index));
                }
                //create a new instance of the service view model and return that view
                var serviceVM= new ServiceViewModel {VehicleId=id};
                return View (serviceVM);
            }

            //POST/Service/Create
            [HttpPost]
            public IActionResult AddService (ServiceViewModel serviceVM)
            {
                if (ModelState.IsValid)
                {
                    var service=new Service
                    {
                    //add our details 
                    VehicleId=serviceVM.VehicleId,
                    ServiceProvided=serviceVM.ServiceProvided,
                    StaffMember=serviceVM.StaffMember,
                    DateofService=serviceVM.DateofService,
                    Cost=serviceVM.Cost,
                    Mileage=serviceVM.Mileage

                };
                svc.AddService(service);
                
                Alert ("Service Added", AlertType.info);
                return RedirectToAction (nameof (Details), new {Id=service.VehicleId});
                }
                
                //redisplay form for editing
                return View (serviceVM);
            }
   
}
    
}