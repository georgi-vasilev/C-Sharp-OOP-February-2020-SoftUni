namespace EasterRaces.Models.Drivers.Entities
{
    using System;
    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Utilities.Messages;

    public class Driver : IDriver
    {
        private string name;
        private bool canParticipate;

        public Driver(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    string excMsg = string.Format
                        (ExceptionMessages.InvalidName, value, 5);

                    throw new ArgumentException(excMsg);
                }
                this.name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate
        {
            get
            {
                return this.canParticipate;
            }
            private set
            {
                if (!this.Car.Equals(null))
                {
                    this.canParticipate = value;
                }
                this.canParticipate = value;    
            }
        }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException
                    (ExceptionMessages.CarInvalid);
            }

            this.Car = car;
            this.CanParticipate = true;
        }
        public void WinRace()
        {
            this.NumberOfWins += 1;
        }
    }
}
