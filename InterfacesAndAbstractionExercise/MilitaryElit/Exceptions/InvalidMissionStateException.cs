using System;

namespace MilitaryElit.Exceptions
{
    public class InvalidMissionStateException : Exception
    {
        public const string DEF_EXC_MSG = "Invalid mission state!";

        public InvalidMissionStateException()
            : base(DEF_EXC_MSG)
        {
        }

        public InvalidMissionStateException(string message)
            : base(message)
        {
        }
    }
}
