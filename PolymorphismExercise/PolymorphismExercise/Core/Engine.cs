using System;
using System.Linq;
using Vehicles.Core.Contracts;
using Vehicles.Factories;
using Vehicles.Models;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private VehicleFactory vehicleFactory;

        public Engine()
        {
            this.vehicleFactory = new VehicleFactory();
        }

        public void Run()
        {
            Vehicle car = ProduceVehicle();
            Vehicle truck = ProduceVehicle();
            Vehicle bus = ProduceVehicle();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] cmdArgs = Console.ReadLine()
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .ToArray();
                try
                {
                    ProcessCommand(car, truck, bus, cmdArgs);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }
            Console.WriteLine(car);
            Console.WriteLine(truck);
            Console.WriteLine(bus);
        }

        private static void ProcessCommand(Vehicle car, Vehicle truck, Vehicle bus, string[] cmdArgs)
        {
            string cmdType = cmdArgs[0];
            string vehicleType = cmdArgs[1];
            double arg = double.Parse(cmdArgs[2]);

            if (cmdType == "Drive")
            {
                if (vehicleType == "Car")
                {
                    Console.WriteLine(car.Drive(arg));
                }
                else if (vehicleType == "Truck")
                {
                    Console.WriteLine(truck.Drive(arg));
                }
                else if (vehicleType == "Bus")
                { 
                    Console.WriteLine(bus.Drive(arg));
                }

            }
            else if (cmdType == "DriveEmpty")
            {
                Console.WriteLine(bus.DriveEmpty(arg));
            }
            else if (cmdType == "Refuel")
            {
                if (vehicleType == "Car")
                {
                    car.Refuel(arg);
                }
                else if (vehicleType == "Truck")
                {
                    truck.Refuel(arg);
                }
                else if (vehicleType == "Bus")
                {
                    bus.Refuel(arg);
                }
            }
        }

        private Vehicle ProduceVehicle()
        {
            string[] vehicleArgs = Console.ReadLine()
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .ToArray();
            string type = vehicleArgs[0];
            double fuelQty = double.Parse(vehicleArgs[1]);
            double fuelConsumption = double.Parse(vehicleArgs[2]);
            double tankCapacity = double.Parse(vehicleArgs[3]);

            if (tankCapacity < fuelQty)
            {
                tankCapacity = 0;
            }

            Vehicle vehicle = this.vehicleFactory.ProduceVehicle(type, fuelQty, fuelConsumption, tankCapacity);

            return vehicle;
        }
    }
}
