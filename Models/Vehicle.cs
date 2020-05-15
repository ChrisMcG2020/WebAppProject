using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace VMS.Data.Models
{      
    public class Vehicle
    {
        //Set the attributes for the Vehicle class
        //Primary key 
        public int Id { get; set; }
  
        [Required]
        public string Make { get; set; }
  
        [Required]
        public string Model { get; set; }

        public string RegNumber { get; set; }
        
        //Only return the date not the date and time
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime DateOfRegistration { get; set; }
  
        //read only way to get the year
        private int Age => (DateTime.Now - DateOfRegistration).Days / 365;
        public int EngineCC { get; set; }
  
        public string Transmission { get; set; }
  
        public string Co2Rating { get; set; }
  
        public string FuelType { get; set; }
  
        public string BodyType { get; set; }
  
        public int NoOfDoors { get; set; }
  
        public string PhotoURL { get; set; }
        
        //Foreign key 
        public int VehicleId { get; set; }

        //Navigation to the service class
        public ICollection<Service> Services { get; set; }
    }
}