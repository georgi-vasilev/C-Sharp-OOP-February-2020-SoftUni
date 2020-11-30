using MilitaryElit.IO.Contracts;
using System;

namespace MilitaryElit.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
