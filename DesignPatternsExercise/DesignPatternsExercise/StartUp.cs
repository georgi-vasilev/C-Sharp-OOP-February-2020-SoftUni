namespace DesignPatternsExercise
{
    using PrototypePattern;

    public class StartUp
    {
        public static void Main()
        {
            SandwichMenu menu = new SandwichMenu();

            menu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");
            menu["PB&J"] = new Sandwich("White", "", 
                "", "Peanut butter and jelly");

            Sandwich bltSandwich1 = menu["BLT"].Clone() as Sandwich;
            Sandwich pbjSandwich1 = menu["PB&J"].Clone() as Sandwich;

        }
    }
}
