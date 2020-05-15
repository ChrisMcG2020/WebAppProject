using System;
using Xunit;

// importing dependencies from the Data Project
using VMS.Data.Models;
using VMS.Data.Services;

namespace VMS.Test
{
    public class TestVehicleService
    {
       // define a set of appropriate tests to test the vehicle service class
       private readonly IVehicleService svc;

       public TestVehicleService()
       {
           svc = new VehicleDbService();
           
           //ensure database is created before each test
           svc.Initialise();
       }

         //Test that if the Vehicle identified by Id exists it is returned
      [Fact]
        public void ifVehicleExists_ShouldReturnTrue()
        { 
        //act
        var newVehicle=svc.AddVehicle(
            new Vehicle
            {
               Make="Ford",
               Model="Focus",
               RegNumber="12345HHH",
               DateOfRegistration=new DateTime(1999,01,01),
               Transmission="Auto",
               Co2Rating="105g/km",
               FuelType="Petrol",
               BodyType="Saloon",
               NoOfDoors=5,
               EngineCC=1800,
               PhotoURL="www.cars.com"
            }
                );

        var vehicle=svc.GetVehicleById(1);
       
       //assert
        Assert.NotNull(vehicle);
        }

        //Test that if vehicle  does not exist it is not retruned
        [Fact]
          public void ifVehicleDoesNotExist_ItIsNotReturned()
          {
            //act
            var vehicle=svc.GetVehicleById(0);

            //assert
            Assert.Null(vehicle);
          }

       [Fact]
         public void AddVehicle_WhenNone_ShouldSetAllAttributes()
         {

           //ACT
           //add a new vehicle to the database
           var vehicle= svc.AddVehicle(
               new Vehicle
               {
                   Make = "Hyundai",
                   Model = "i30",
                   RegNumber = "FHY502",
                   DateOfRegistration = new DateTime(2017,01,01),
                   EngineCC = 1600,
                   Transmission = "Manual",
                   Co2Rating = "126 g/Km",
                   FuelType = "Petrol",
                   BodyType = "Hatchback",
                   NoOfDoors = 5,
                   PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
               }
               );
           
           // Retrieve the vehicle from the database
           var v = svc.GetVehicleById(vehicle.Id);
           
           //Assert that the vehicle is not null
           Assert.NotNull(v);
           
           //Assert that the properties of the vehicle were set properly
           Assert.Equal(v.Id, v.Id);
           Assert.Equal("Hyundai",v.Make);
           Assert.Equal("i30",v.Model);
           Assert.Equal("FHY502",v.RegNumber);
           Assert.Equal(new DateTime(2017,01,01),v.DateOfRegistration);
           Assert.Equal(1600,v.EngineCC);
           Assert.Equal("Manual", v.Transmission);
           Assert.Equal("126 g/Km", v.Co2Rating);
           Assert.Equal("Petrol", v.FuelType);
           Assert.Equal("Hatchback", v.BodyType);
           Assert.Equal(5, v.NoOfDoors);
           Assert.Equal("https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg",v.PhotoURL);
       }

       [Fact]
       public void UpdateVehicle_ThatExists_ShouldUpdateAllProperties()
       {
           //Create a vehicle to test
           var vehicle = svc.AddVehicle(
               new Vehicle
               {
                   Make = "Hyundai",
                   Model = "i30",
                   RegNumber = "FHY502",
                   DateOfRegistration = new DateTime (2017,01,01),
                   EngineCC = 1600,
                   Transmission = "Manual",
                   Co2Rating = "126 g/Km",
                   FuelType = "Petrol",
                   BodyType = "Hatchback",
                   NoOfDoors = 5,
                   PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
               }
           );
           
           //ACT
           //update the test vehicle
           var updatedVehicle = new Vehicle
           {
                   Make = "LandRover",
                   Model = "Discovery",
                   RegNumber = "142DL1234",
                   DateOfRegistration = new DateTime(2014,01,01),
                   EngineCC = 2000,
                   Transmission = "Automatic",
                   Co2Rating = "203 g/Km",
                   FuelType = "Diesel",
                   BodyType = "Jeep",
                   NoOfDoors = 5,
                   PhotoURL = "https://cdn.motor1.com/images/mgl/6Jy3E/s1/vw-touareg-vs-land-rover-discovery.jpg"
           };

           updatedVehicle = svc.UpdateVehicle(vehicle.Id, updatedVehicle);
           
           //Assert that the updated vehicle is not null
           Assert.NotNull(updatedVehicle);

           // Assert that the updated vehicle's properties were set properly
           Assert.Equal("LandRover",updatedVehicle.Make);
           Assert.Equal("Discovery",updatedVehicle.Model);
           Assert.Equal("142DL1234",updatedVehicle.RegNumber);
           Assert.Equal(new DateTime (2014,01,01) ,updatedVehicle.DateOfRegistration);
           Assert.Equal(2000,updatedVehicle.EngineCC);
           Assert.Equal("Automatic", updatedVehicle.Transmission);
           Assert.Equal("203 g/Km", updatedVehicle.Co2Rating);
           Assert.Equal("Diesel", updatedVehicle.FuelType);
           Assert.Equal("Jeep", updatedVehicle.BodyType);
           Assert.Equal(5, updatedVehicle.NoOfDoors);
           Assert.Equal("https://cdn.motor1.com/images/mgl/6Jy3E/s1/vw-touareg-vs-land-rover-discovery.jpg",updatedVehicle.PhotoURL);
       }

       [Fact]
       public void GetAllVehicles_WhenNone_ShouldReturn0()
       {
           //Act 
           //get all vehicles in the database
           var vehicles = svc.GetAllVehicles();
           var count = vehicles.Count;
           
           //Assert
           //No vehicles have been added so should return 0
           Assert.Equal(0,count);
       }

       [Fact]
       public void GetVehicle_WhenAdded_ShouldReturnTatVehicle()
       {
           //Act
           //add a vehicle to the dataabase
           var v = svc.AddVehicle(
               new Vehicle
               {
                   Make = "Hyundai",
                   Model = "i30",
                   RegNumber = "FHY502",
                   DateOfRegistration = new DateTime (2017,01,01),
                   EngineCC = 1600,
                   Transmission = "Manual",
                   Co2Rating = "126 g/Km",
                   FuelType = "Petrol",
                   BodyType = "Hatchback",
                   NoOfDoors = 5,
                   PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
               }
           );

           //retrieve the vehicle 
           var vehicle = svc.GetVehicleById(v.Id);
           
           //Assert
           //check the vehicle  exists
           Assert.NotNull(vehicle);
           //check that the Vehicle we have retrieved from the database  is equal to the vehicle we have just added
           Assert.Equal(v.Id,vehicle.Id);
       }


      //Test to delete a vehicle should work if that vehicle exists
      [Fact]
        public void DeleteVehicle_WhenExists_ShouldReturnTrue()
        {
          //Act
          //add a new vehicle to the database

            var v = svc.AddVehicle(
               new Vehicle
               {
                 Make = "Hyundai",
                 Model = "i30",
                 RegNumber = "FHY502",
                 DateOfRegistration = new DateTime(2017,01,01),
                 EngineCC = 1600,
                 Transmission = "Manual",
                 Co2Rating = "126 g/Km",
                 FuelType = "Petrol",
                 BodyType = "Hatchback",
                 NoOfDoors = 5,
                 PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
               }
            );
            //delete the vehicle
              var deleted=svc.DeleteVehicle(v.Id);

                 //assert 
                 //Should retrun true if has been deleted
                 Assert.True (deleted);
         }
        

       [Fact]
       
          public void DeleteVehicle_ThatDoesNotExist_ShouldNotWork()
          {
            //act
            //Try to delete a vehicle with an Id that has not been created
            var deleted=svc.DeleteVehicle(0);

            //assert
            //Will return false as that vehicle cannotbe deleted
             Assert.False (deleted);
          }

       
       [Fact]
          public void AddService_WhenNoVehicleExists_ShouldNotWork()
       {
            var s = svc.AddService(new Service
             {
                VehicleId = 1,
                ServiceProvided= "Timing Belt",
                StaffMember= "Paul Leonard",
                DateofService = new DateTime(2019,06,01),
                 Cost= 500,
                 Mileage = 125000
             });
           
           //vehicle does not exist so should return null
           Assert.Null(s);
           
       }

       [Fact]
            public void AddService_WhenVehicleExists_ShouldWork()
             {
             //Act
             //Add vehicle and service
               var v = svc.AddVehicle(
                 new Vehicle
               {
                  Make = "Hyundai",
                  Model = "i30",
                  RegNumber = "FHY502",
                  DateOfRegistration = new DateTime (2017,01,01),
                  EngineCC = 1600,
                  Transmission = "Manual",
                  Co2Rating = "126 g/Km",
                  FuelType = "Petrol",
                  BodyType = "Hatchback",
                  NoOfDoors = 5,
                  PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
               }
            );
           
               var s = svc.AddService(new Service
                {
                  VehicleId = v.Id,
                  ServiceProvided= "Timing Belt",
                  StaffMember= "Paul Leonard",
                  DateofService = new DateTime(2019,06,01),
                  Cost= 500,
                  Mileage = 125000
                });
 
              //Assert
              //Should not be bull as vehicle exists
              Assert.NotNull(s);
           
       }

       [Fact]
       public void GetService_WhenAdded_ShouldReturnService()
       {
           //Act
           //Add vehicle and service
            var v = svc.AddVehicle(
                new Vehicle
                {
                  Make = "Hyundai",
                  Model = "i30",
                  RegNumber = "FHY502",
                  DateOfRegistration = new DateTime (2017,01,01),
                  EngineCC = 1600,
                  Transmission = "Manual",
                  Co2Rating = "126 g/Km",
                  FuelType = "Petrol",
                  BodyType = "Hatchback",
                  NoOfDoors = 5,
                  PhotoURL = "https://cdn.motor1.com/images/mgl/mjm7r/s1/2017-hyundai-i30.jpg"
                }
              );
            
               var s = svc.AddService(new Service
                {
                 VehicleId = v.Id,
                 ServiceProvided= "Timing Belt",
                 StaffMember= "Paul Leonard",
                 DateofService = new DateTime(2019,06,01),
                 Cost= 500,
                 Mileage = 125000
                }
              );
           

                 var service = svc.GetServiceById(s.Id);
                 Assert.NotNull(s);
       }
     
    }
}