using Vehicles.Core;
using Vehicles.Core.Contracts;

namespace PolymorphismExercise
{
    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
