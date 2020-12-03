namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Repositories.Contracts;

    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> models;

        public CarRepository()
        {
            this.models = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models
            => this.models.AsReadOnly();

        public void Add(ICar model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
            => this.models.AsReadOnly();

        public ICar GetByName(string name)
            => this.models
            .Where(x => x.Model == name)
            .FirstOrDefault();

        public bool Remove(ICar model)
            => this.models.Remove(model);
    }
}
