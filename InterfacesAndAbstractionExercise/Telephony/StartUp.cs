using Telephony.Contracts;
using Telephony.Core;
using Telephony.IO;

namespace Telephony
{
    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
