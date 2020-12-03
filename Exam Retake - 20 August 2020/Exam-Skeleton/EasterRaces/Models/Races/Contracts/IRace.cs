namespace EasterRaces.Models.Races.Contracts
{
    using System.Collections.Generic;
    using EasterRaces.Models.Drivers.Contracts;

    public interface IRace
    {
        string Name { get; }

        int Laps { get; }

        IReadOnlyCollection<IDriver> Drivers { get; }

        void AddDriver(IDriver driver);
    }
}