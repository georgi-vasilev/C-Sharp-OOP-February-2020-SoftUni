namespace PlayersAndMonsters.Wizards
{
    public abstract class Wizard : Hero
    {
        public  Wizard(string username, int level) 
            : base(username, level)
        {
        }
    }
}
