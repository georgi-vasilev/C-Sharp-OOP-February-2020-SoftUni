namespace TemplatePattern
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            Sourdough sourdough = new Sourdough();
            sourdough.Make();
            Console.WriteLine();

            TwelveGrain twelveGrain = new TwelveGrain();
            twelveGrain.Make();
            Console.WriteLine();

            WhiteWheat whiteWheat = new WhiteWheat();
            whiteWheat.Make();
        }
    }
}
