using Raiding.Models;
using Raiding.Models.Contracts;

namespace Raiding.Factories
{
    public class HeroFactory
    {
        private const string EXC_MSG = "Invalid hero!";
        public IHero ProduceHero(string type, string name)
        {
            IHero hero = null;

            if (type == "Druid")
            {
                hero = new Druid(name);
            }
            else if (type == "Paladin")
            {
                hero = new Paladin(name);
            }
            else if (type == "Rogue")
            {
                hero = new Rogue(name);
            }
            else if (type == "Warrior")
            {
                hero = new Warrior(name);
            }

            return hero;
        }
    }
}
