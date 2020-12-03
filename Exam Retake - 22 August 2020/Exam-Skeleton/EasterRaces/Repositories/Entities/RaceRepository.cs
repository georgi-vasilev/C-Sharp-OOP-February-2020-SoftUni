namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using EasterRaces.Models.Races.Contracts;
    using EasterRaces.Repositories.Contracts;

    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> models;

        public RaceRepository()
        {
            this.models = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models
            => this.models.AsReadOnly();

        public void Add(IRace model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
            => this.models.AsReadOnly();

        public IRace GetByName(string name)
            => this.models
            .Where(x => x.Name == name)
            .FirstOrDefault();

        public bool Remove(IRace model)
            => this.models.Remove(model);
    }
}
