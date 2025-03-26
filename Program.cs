using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

abstract class Vehicle
{
    public  string Make { get; set; }

    public  string Model { get; set; }

    public int Year { get; set; }

    public Vehicle(string make, string model, int year) 
    {
        Make = make;
        Model = model;
        Year = year;
    }

    public abstract void StartTheEngine();

    public override string ToString()
    {
        return $" {Make} {Model}";
    }
}

class Car : Vehicle
{
    public int Doors { get; set; }

    public Car(string make, string model,int year, int doors) : base(make, model, year)
    {
        Doors = doors;
        
    }
    public override void StartTheEngine()
    {
        Console.WriteLine("The engine starts with a press of a button");
    }

    public Car Clone()
    {
        return new Car(Make, Model, Year, Doors);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Doors: {Doors} ";
    }
}

class Plane : Vehicle
{
    public int Wingspan { get; set; }

    public Plane(int wingspan, string make, string model, int year) : base(make, model, year)
    {
        Wingspan = wingspan;


    }
    public override void StartTheEngine()
    {
        Console.WriteLine("I have no clue how to start the engine");
    }

    public Plane Clone()
    {
        return new Plane(Wingspan, Make, Model, Year);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Wingspan: {Wingspan} meters";
    }

}

class Garage : IEnumerable<Vehicle>
{
    private List<Vehicle> vehicles;

    public Garage()
    {
        vehicles = new List<Vehicle>();
    }

    public void AddVehicle(Vehicle vehicle) 
    { 
        vehicles.Add(vehicle);
    }
    public IEnumerator<Vehicle> GetEnumerator()
    {
        foreach (var vehicle in vehicles)
        {
            yield return vehicle;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


class Program
{
    static void Main()
    {
        Garage garage = new Garage();

        
        Car car1 = new Car("Mercedes", "CLK", 2006, 2);
        Car car2 = (Car)car1.Clone(); 

        Plane plane1 = new Plane(35, "Boeing", "747", 2018);
        Plane plane2 = (Plane)plane1.Clone(); 

        garage.AddVehicle(car1);
        garage.AddVehicle(car2);
        garage.AddVehicle(plane1);
        garage.AddVehicle(plane2);

        foreach (var vehicle in garage)
        {
            Console.WriteLine(vehicle);
            vehicle.StartTheEngine();
        }
    }
}