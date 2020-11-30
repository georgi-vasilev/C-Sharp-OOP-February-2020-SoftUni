namespace Raiding.Models
{
    public class Rogue : BaseHero
    {
        private const int POWER_CONST = 80;

        public Rogue(string name) 
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
