namespace EasterRaces.Models.Cars.Entities
{
    using System;
    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Utilities.Messages;

    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;

        protected Car(string model, int horsePower, double cubicCentimeters, int minimumHorsePower, int maximumHorsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
            this.MinimumHorsePower = minimumHorsePower;
            this.MaximumHorsePower = maximumHorsePower;
        }

        public string Model
        {
            get
            {
                return this.model;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    string excMSg = string.Format(ExceptionMessages.InvalidModel, value, 4);
                    throw new ArgumentException(excMSg);
                }

                this.model = value;
            }
        }

        public int HorsePower { get; protected set; }

        public int MinimumHorsePower { get; private set; }
        public int MaximumHorsePower { get; private set; }

        public double CubicCentimeters { get; private set; }



        public double CalculateRacePoints(int laps)
        {
            double racePoints = this.CubicCentimeters / (this.horsePower * laps);

            return racePoints;
        }
    }
}
