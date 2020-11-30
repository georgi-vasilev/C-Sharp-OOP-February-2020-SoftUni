namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int POWER_CONST = 80;

        public Druid(string name)
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
