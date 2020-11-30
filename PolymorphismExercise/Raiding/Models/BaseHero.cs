using Raiding.Models.Contracts;

namespace Raiding.Models
{
    public abstract class BaseHero : IHero
    {
        protected BaseHero(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public abstract int Power { get; }

        public virtual string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name}";
        }
    }
}
