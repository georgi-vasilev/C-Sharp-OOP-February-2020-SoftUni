namespace EasterRaces.Core.Entities
{
    using System;
    using System.Linq;
    using System.Text;
    using EasterRaces.Core.Contracts;
    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Models.Cars.Entities;
    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Models.Drivers.Entities;
    using EasterRaces.Models.Races.Contracts;
    using EasterRaces.Models.Races.Entities;
    using EasterRaces.Repositories.Contracts;
    using EasterRaces.Repositories.Entities;
    using EasterRaces.Utilities.Messages;

    public class ChampionshipController : IChampionshipController
    {
        private IRepository<ICar> cars = new CarRepository();
        private IRepository<IDriver> drivers = new DriverRepository();
        private IRepository<IRace> races = new RaceRepository();

        public ChampionshipController()
        {
        }

        public string AddCarToDriver(string driverName, string carModel)
        {

            ICar car = this.cars.GetByName(carModel);
            IDriver driver = this.drivers.GetByName(driverName);

            if (driver == null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.DriverNotFound, driverName);

                throw new InvalidOperationException(excMsg);
            }
            if (car == null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.CarNotFound, carModel);

                throw new InvalidOperationException(excMsg);
            }

            driver.AddCar(car);
            string result = string.Format
                (OutputMessages.CarAdded, driverName, carModel);

            return result;
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IDriver driver = this.drivers.GetByName(driverName);
            IRace race = this.races.GetByName(raceName);

            if (driver == null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.DriverNotFound, driverName);

                throw new InvalidOperationException(excMsg);
            }
            if (race == null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.RaceNotFound, raceName);

                throw new InvalidOperationException(excMsg);
            }

            race.AddDriver(driver);
            string result = string.Format
                (OutputMessages.DriverAdded, driverName, raceName);

            return result;
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            string carModel = this.cars.GetByName(model)?.Model.ToString();
            if (carModel != null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.CarExists, model);
                throw new InvalidOperationException(excMsg);
            }

            Car car;

            if (type.Equals("Muscle"))
            {
                car = new MuscleCar(model, horsePower);
            }
            else
            {
                car = new SportsCar(model, horsePower);
            }

            this.cars.Add(car);
            string result = string.Format(OutputMessages.CarCreated,
                car.GetType().Name, model);

            return result;
        }

        public string CreateDriver(string driverName)
        {
            string driverN = this.drivers.GetByName(driverName)?.Name.ToString();

            if (driverN != null)
            {
                string excMsg = string.Format
                    (ExceptionMessages.DriversExists, driverName);
                throw new InvalidOperationException(excMsg);
            }

            Driver driver = new Driver(driverName);
            this.drivers.Add(driver);

            string result = string.Format
                (OutputMessages.DriverCreated, driverName);
            return result;
        }

        public string CreateRace(string name, int laps)
        {
            string raceName = this.races.GetByName(name)?.Name.ToString();
            if (raceName != null)
            {
                string excMsg = string.Format
                   (ExceptionMessages.RaceExists, name);
                throw new InvalidOperationException(excMsg);
            }

            Race race = new Race(name, laps);
            this.races.Add(race);

            string result = string.Format
                (OutputMessages.RaceCreated, name);
            return result;
        }

        public string StartRace(string raceName)
        {
            IRace race = this.races.GetByName(raceName);
            int minParticipants = 3;

            if (race == null)
            {
                string excMsg = string.Format
                   (ExceptionMessages.RaceNotFound, raceName);
                throw new InvalidOperationException(excMsg);
            }
            if (race.Drivers.Count < minParticipants)
            {
                string excMsg = string.Format
                  (ExceptionMessages.RaceInvalid, raceName, minParticipants);
                throw new InvalidOperationException(excMsg);
            }

            var winners = race.Drivers
                .OrderByDescending(d => d.Car
                .CalculateRacePoints(race.Laps))
                .Take(3)
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine(string.Format(OutputMessages.DriverFirstPosition,
                winners.ElementAt(0).Name, raceName))
                .AppendLine(string.Format(OutputMessages.DriverSecondPosition,
                winners.ElementAt(1).Name, raceName))
                .AppendLine(string.Format(OutputMessages.DriverThirdPosition,
                winners.ElementAt(2).Name, raceName));

            string result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
