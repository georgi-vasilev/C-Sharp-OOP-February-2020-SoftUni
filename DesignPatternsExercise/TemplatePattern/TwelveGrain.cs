namespace TemplatePattern
{
    using System;

    public class TwelveGrain : Bread
    {
        public TwelveGrain()
        {
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering ingridients for 12-grain bread!");
        }

        public override void Bake()
        {
            Console.WriteLine("Baking the 12-grain bread! (25 minutes)");
        }
    }
}
