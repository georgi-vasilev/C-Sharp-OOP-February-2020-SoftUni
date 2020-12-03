namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Repositories.Contracts;

    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> models;

        public DriverRepository()
        {
            this.models = new List<IDriver>();
        }

        public IReadOnlyCollection<IDriver> Models
            => this.models.AsReadOnly();

        public void Add(IDriver model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
            => this.models.AsReadOnly();

        public IDriver GetByName(string name)
            => this.models
            .Where(x => x.Name == name)
            .FirstOrDefault();

        public bool Remove(IDriver model)
            => this.models.Remove(model);
    }
}
