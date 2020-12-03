using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;

        [SetUp]
        public void Setup()
        {
            this.raceEntry = new RaceEntry();
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            int expectedCount = 0;

            RaceEntry raceEntry = new RaceEntry();

            Assert.AreEqual(expectedCount, raceEntry.Counter);
        }

        [Test]
        public void AddDriverShouldThrowExceptionWhenDriverIsNull()
        {
            UnitDriver unitDriver = null;


            Assert.That(() => 
            {
                this.raceEntry.AddDriver(unitDriver);

            },Throws
            .InvalidOperationException
            .With.Message.EqualTo("Driver cannot be null."));
        }

        [Test]
        public void AddingExistingDriverShouldThrowException()
        {
            UnitCar car = new UnitCar("Porsche", 350, 5000);
            UnitDriver unitDriver = new UnitDriver("Pesho", car);

            this.raceEntry.AddDriver(unitDriver);

            Assert.That(() =>
            {
                this.raceEntry.AddDriver(unitDriver);

            }, Throws
            .InvalidOperationException
            .With.Message.EqualTo($"Driver {unitDriver.Name} is already added."));
        }


        //TODO: Fix this one cuz you are definetly doing something wrong...
        [Test]
        public void AddingDriverShouldReturnTheCorrectMessage()
        {
            UnitCar car = new UnitCar("Porsche", 350, 5000);
            UnitDriver driver = new UnitDriver("Ivan", car);

            string actualOutputMessage = this.raceEntry.AddDriver(driver).ToString();
            string expectedOutputMessage = $"Driver {driver.Name} added in race.";

            Assert.AreEqual(expectedOutputMessage, actualOutputMessage);  
        }

        [Test]
        public void CalculateAverageHorsePowerShouldThrowExceptionWhenInvalid()
        {
            const int minParticipants = 2;
            Assert.That(() => 
            {
                this.raceEntry.CalculateAverageHorsePower();

            }, Throws
            .Exception.With.Message.EqualTo
            ($"The race cannot start with less than {minParticipants} participants."));
        }
        
        [Test]
        public void CalculateAverageHorsePowerShouldReturnCorrectNumber()
        {
            UnitCar carP = new UnitCar("Porsche", 350, 5000);
            UnitDriver driverP = new UnitDriver("Pesho", carP);
            
            UnitCar carG = new UnitCar("Mustang", 450, 5000);
            UnitDriver driverG = new UnitDriver("Gosho", carG);

            UnitCar carT = new UnitCar("Mazda", 280, 5000);
            UnitDriver driverT = new UnitDriver("Tosho", carT);

            ICollection<UnitDriver> tmp = new List<UnitDriver>();
            tmp.Add(driverT);
            tmp.Add(driverG);
            tmp.Add(driverP);

            this.raceEntry.AddDriver(driverP);
            this.raceEntry.AddDriver(driverG);
            this.raceEntry.AddDriver(driverT);

            double actualAverageHP =
                this.raceEntry.CalculateAverageHorsePower();
            double expectedAverageHp = tmp
                .Select(x => x.Car.HorsePower)
                .Average();

            Assert.AreEqual(expectedAverageHp, actualAverageHP);
        }
    }
}