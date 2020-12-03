namespace EasterRaces.Models.Races.Entities
{
    using System;
    using System.Collections.Generic;
    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Models.Races.Contracts;
    using EasterRaces.Utilities.Messages;

    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;

        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers = new List<IDriver>();
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

        public int Laps
        {
            get
            {
                return this.laps;
            }
            private set
            {
                if (value < 1)
                {
                    throw new AggregateException
                        (ExceptionMessages.InvalidNumberOfLaps);
                }
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers.AsReadOnly();

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);
            }
            else if (driver.CanParticipate.Equals(false))
            {
                string expMsg = string.Format(ExceptionMessages.DriverNotParticipate, driver.Name);
                throw new ArgumentException(expMsg);
            }
            else if (this.drivers.Contains(driver))
            {
                string expMsg = string.Format(ExceptionMessages.
                    DriverAlreadyAdded, driver.Name, this.Name);
                throw new ArgumentNullException(expMsg);
            }

            this.drivers.Add(driver);
        }
    }
}
