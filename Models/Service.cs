using System;
using System.ComponentModel.DataAnnotations;

namespace VMS.Data.Models
{
    public class Service
    {     
        //Set the attributes for the Service class
        public int Id { get; set; }
        
        public string ServiceProvided { get; set; }
  
        public string StaffMember { get; set; }

        //Only return the date not the date and time
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime DateofService { get; set; }

        public decimal Cost { get; set; }
        public int Mileage { get; set; }

        //Foreign key 
        public int VehicleId { get; set; }

        public string RegNumber { get; set; }
        //Navigation to the vehicle class
        public Vehicle Vehicle { get; set; }
    }
}