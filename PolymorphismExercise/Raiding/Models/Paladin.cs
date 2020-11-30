namespace Raiding.Models
{
    public class Paladin : BaseHero
    {
        private const int POWER_CONST = 100;

        public Paladin(string name) 
            : base(name)
        {
        }

        public override int Power { get => POWER_CONST; }

        public override string CastAbility()
        {
            return base.CastAbility() + $" healed for {this.Power}";
        }
    }
}
