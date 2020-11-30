namespace TemplatePattern
{
    using System;

    public class Sourdough : Bread
    {
        public Sourdough()
        {
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering ingredients for Sourdough Bread!");
        }

        public override void Bake()
        {
            Console.WriteLine("Baking the Sourdough Bread! (20 minutes)");
        }
    }
}
