# COM741 Assignment 2019-20

## Last Updated 22-03-2020

This template should be used to complete Assignment 1 to create the Vehicle Management System

## Project Structure
The solution contains three projects

1. VMS.Data - the data project uses EntityFramework Core 3.1 and the Sqlite database driver. It is used to implement the entity Models, Entityframework Repository (context) and VehicleService

2. VMS.Test - the xunit test project is used to test the functionality in the VehicleService class exposed in the VMS.Data project

3. VMS.Web - is an Mvc project used to implement a Web based user interface to the Vehicle Management System.
