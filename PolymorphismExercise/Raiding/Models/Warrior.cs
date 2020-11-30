namespace Raiding.Models
{
    public class Warrior : BaseHero
    {
        private const int POWER_CONST = 100;

        public Warrior(string name) 
            : base(name)
        {
        }

        public override int Power { get => POWER_CONST; }

        public override string CastAbility()
        {
            return base.CastAbility() + $" hit for {this.Power} damage";
        }
    }
}
