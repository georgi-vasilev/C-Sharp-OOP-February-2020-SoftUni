using System;
using Vehicles.Common;
using Vehicles.Models.Contracts;

namespace Vehicles.Models
{
    public abstract class Vehicle : IDrivable, IRefuelable
    {
        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
            this.TankCapacity = tankCapacity;
        }

        public virtual double FuelQuantity { get; protected set; }

        public virtual double FuelConsumption { get; protected set; }

        public double TankCapacity{ get; private set; }

        public virtual string Drive(double kilometers)
        {
            double fuelNeeded = kilometers * this.FuelConsumption;

            if (this.FuelQuantity < fuelNeeded)
            {
                string excMsg = String.Format(ExceptionMessages.NotEnoughFuelExceptionMessage,
                    this.GetType().Name);
                throw new InvalidOperationException(excMsg);
            }

            this.FuelQuantity -= fuelNeeded;

            return $"{this.GetType().Name} travelled {kilometers} km";
        }

        public string DriveEmpty(double kilometers)
        {
            double fuelNeeded = kilometers * this.FuelConsumption;

            if (this.FuelQuantity < fuelNeeded)
            {
                string excMsg = String.Format(ExceptionMessages.NotEnoughFuelExceptionMessage,
                    this.GetType().Name);
                throw new InvalidOperationException(excMsg);
            }

            this.FuelQuantity -= fuelNeeded;

            return $"{this.GetType().Name} travelled {kilometers} km";
        }

        public virtual void Refuel(double fuelAmount)
        {
            double availableSpace = this.TankCapacity - this.FuelQuantity;

            if (fuelAmount <= 0)
            {
                string excMsg = String.Format(ExceptionMessages.InvalidValueOfFuel);
                throw new InvalidOperationException(excMsg);
            }
            else if (fuelAmount > availableSpace)
            {
                string excMsg = String.Format(ExceptionMessages.InvalidFuelQuantity,
                    fuelAmount);
                throw new InvalidOperationException(excMsg);
            }

            this.FuelQuantity += fuelAmount;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
