using System;
using System.Collections.Generic;
using Raiding.Core.Contracts;
using Raiding.Models.Contracts;
using Raiding.Factories;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private ICollection<IHero> heroes;
        private HeroFactory heroFactory;
        private List<string> heroTypes = new List<string>()
        {
            "Paladin",
            "Druid",
            "Warrior",
            "Rogue"
        };

        public Engine()
        {
            this.heroes = new List<IHero>();
            this.heroFactory = new HeroFactory();
        }

        public void Run()
        {

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                IHero hero = null;

                string name = Console.ReadLine();
                string type = Console.ReadLine();

                if (heroTypes.Contains(type))
                {
                    hero = this.heroFactory.ProduceHero(type, name);
                    this.heroes.Add(hero);
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                }

            }

            int bossPower = int.Parse(Console.ReadLine());

            int raidGroupPower = 0;

            foreach (IHero hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
                raidGroupPower += hero.Power;
            }


            if (raidGroupPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }

        }
    }
}
