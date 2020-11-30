using MilitaryElit.Core;
using MilitaryElit.Core.Contracts;
using MilitaryElit.IO;
using MilitaryElit.IO.Contracts;

namespace MilitaryElit
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
