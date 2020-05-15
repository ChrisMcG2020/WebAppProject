using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using VMS.Data.Models;
using VMS.Data.Repositories;

namespace VMS.Data.Services
{
    // implement each of these methods 
    public class VehicleDbService : IVehicleService
    {
     private readonly VehicleDbContext  db;

        public VehicleDbService()
        {
            db=new VehicleDbContext();
        } 
        public void Initialise()
        {
           db.Initialise();
        }
       
       
        // Vehicle Management
        public Vehicle AddVehicle(Vehicle v)
        {
            //Verify that the vehicle does not already exist and if it does return null
             var exists = db.Vehicles.FirstOrDefault(r=> r.RegNumber == v.RegNumber);
            if (exists != null)
            {
                return null;
            }
            var vehicle=new Vehicle
            {
              Make=v.Make,
              Model=v.Model,
              RegNumber=v.RegNumber,
              DateOfRegistration=v.DateOfRegistration,
              Transmission=v.Transmission,
              Co2Rating=v.Co2Rating,
              FuelType=v.FuelType,
              BodyType=v.BodyType,
              NoOfDoors=v.NoOfDoors,
              EngineCC=v.EngineCC,
              PhotoURL=v.PhotoURL,};

            //add vehicle to Database and save changes
              db.Vehicles.Add(vehicle);
              db.SaveChanges();
 
              return vehicle;
        }

         public bool DeleteVehicle(int id)
        {
            //Load vehicle from database
            var v = GetVehicleById(id);
            //verify if that Id does exist and return null if not
            if (v == null)
            {
                return false;
            }

            //remove vehicle from database and save the changes
            db.Vehicles.Remove(v);
            db.SaveChanges();

            return true;
        }

        public IList<Vehicle> GetAllVehicles(string orderBy = null)
        {
            //returns all vehicles in database in a list 
            return db.Vehicles
                     .ToList();
        }
  
        public Vehicle GetVehicleById(int id)
        {
            //returns specific vehicle by its Id , include its service details too
            return db.Vehicles
                            .Include(v => v.Services)
                            .FirstOrDefault(v => v.Id==id);
        }
        public Vehicle UpdateVehicle(int id, Vehicle v)
        {
           //find the Vehicle
            var vehicle=GetVehicleById(id);

            //find out if it exists and if not return null
            if (vehicle==null)
            {
                return null;
            }
            //update the details of the vehicle and save the changes 
            vehicle.Make=v.Make;
            vehicle.Model=v.Model;
            vehicle.RegNumber=v.RegNumber;
            vehicle.DateOfRegistration=v.DateOfRegistration;
            vehicle.Transmission=v.Transmission;
            vehicle.Co2Rating=v.Co2Rating;
            vehicle.FuelType=v.FuelType;
            vehicle.BodyType=v.BodyType;
            vehicle.NoOfDoors=v.NoOfDoors;
            vehicle.EngineCC=v.EngineCC;
            vehicle.PhotoURL=v.PhotoURL;

            db.SaveChanges();

            return vehicle;
        }

        // Service Management
         public Service AddService(Service s)
        {
            //verify Vehicle exists and if not return
            var exists=GetVehicleById(s.VehicleId);
            if (exists == null)
            {
                return null;
            }
            var service=new Service
          {
              VehicleId=s.VehicleId,
              ServiceProvided=s.ServiceProvided,
              StaffMember=s.StaffMember,
              DateofService=s.DateofService,
              Cost=s.Cost,
              Mileage=s.Mileage
              };

              db.Services.Add(service);
              db.SaveChanges();

              return service;
          
        }
        public Service GetServiceById(int id)
        {
            //returns service by its id and if does not exist returns null
              var service = db.Services.FirstOrDefault(s => s.Id == id);
              if (service == null)
            {
                return null;
            }

            return service;
        }
        
        public bool DeleteService(int id)
        {
             // check if this service rexists and if not return false
               var service = db.Services.FirstOrDefault(s => s.Id == id );
                                       
               if (service == null)
            {
                return false;
            }
            // delete the selected service and save the changes
            db.Services.Remove(service);
            db.SaveChanges();
            // return service 
            return true;
        }
          //Create a List of all service records tied to a particular vehicle
         public IList<Service> GetAllServiceRecords(string orderBy = null) 
         {
             //return the service record of the vehicle
                return db.Services
                                .Include(s => s.Vehicle)
                                .ToList();
        }

        }

    }
