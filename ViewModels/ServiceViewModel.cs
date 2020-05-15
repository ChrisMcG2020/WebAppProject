using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VMS.Web.ViewModels
{
    public class ServiceViewModel
    {
        // SelectList of vehicles       
        public SelectList Vehicles { set; get; }

        //Form to collect service info
        [Required]
        public int VehicleId { get; set; }
        public string StaffMember { get; set; }
        //Only return the date not the date and time
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime DateofService { get; set; }
        //Extra space for detailed information if needed
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string ServiceProvided { get; set; }
        public int Mileage { get; set; }
        public decimal Cost{ get; set; }
        

    }
}