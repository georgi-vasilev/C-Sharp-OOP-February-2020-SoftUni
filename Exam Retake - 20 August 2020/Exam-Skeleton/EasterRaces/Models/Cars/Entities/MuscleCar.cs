namespace EasterRaces.Models.Cars.Entities
{
    using System;
    using EasterRaces.Utilities.Messages;

    public class MuscleCar : Car
    {
        private const double cubicCm = 5000d;
        private const int minHP = 400;
        private const int maxHP = 600;

        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, cubicCm, minHP, maxHP)
        {
            this.HorsePower = ValidateHorsePower(base.HorsePower);
        }

        private int ValidateHorsePower(int horsePower)
        {
            if (horsePower < minHP || horsePower > maxHP)
            {
                string excMsg = string.Format(ExceptionMessages.InvalidHorsePower, horsePower);
                throw new InvalidOperationException(excMsg);
            }

            return horsePower;
        }
    }
}
