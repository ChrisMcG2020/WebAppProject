using System.Collections.Generic;
using VMS.Data.Models;

namespace VMS.Data.Services
{
    public interface IVehicleService
    {
        // initialise the database
        void Initialise();

        // Retrieve a list of vehicles 
        // optional order parameter allows result list
        // to be ordered by “make”, “fuel” or “registered”
        IList<Vehicle> GetAllVehicles(string orderBy=null);

        // return vehicle (with associated services) identified by id or null if not found
        Vehicle GetVehicleById(int id);

        // delete the vehicle identified by id
        // return true if completed else false
        bool DeleteVehicle(int id);

        // update the vehicle identified by id
        // return true if completed else false
        Vehicle UpdateVehicle(int id, Vehicle v);
            
        // add the new vehicle and return vehicle if successful otherwise return null
        Vehicle AddVehicle(Vehicle v);
        
         // return the service identified by id or null if not found
        Service GetServiceById(int id);

        // add the new service and return service if successful otherwise return null
        Service AddService(Service s);

        // delete the service identified by id and return true if successful otherwise return false 
        bool DeleteService(int id);

        IList<Service> GetAllServiceRecords(string orderBy=null);
        
    }
}