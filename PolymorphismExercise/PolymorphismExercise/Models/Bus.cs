using System;
using Vehicles.Common;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {

        }

        public override double FuelQuantity { get => base.FuelQuantity; protected set => base.FuelQuantity = value; }

        public override double FuelConsumption { get => base.FuelConsumption; protected set => base.FuelConsumption = value; }

        public override string Drive(double kilometers)
        {
            double fuelNeeded = kilometers * (this.FuelConsumption + 1.4);

            if (this.FuelQuantity < fuelNeeded)
            {
                string excMsg = String.Format(ExceptionMessages.NotEnoughFuelExceptionMessage,
                    this.GetType().Name);
                throw new InvalidOperationException(excMsg);
            }

            this.FuelQuantity -= fuelNeeded;

            return $"{this.GetType().Name} travelled {kilometers} km";
        }
    }
}
